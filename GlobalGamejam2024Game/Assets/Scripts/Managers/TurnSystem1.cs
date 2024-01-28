using GDC.Enums;
using Level_1;
using MainGame;
using MainGame.Dialog;
using MainGame.Happy;
using System.Collections;
using AudioPlayer;
using GDC.Managers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level_2
{
    public class TurnSystem1 : MonoBehaviour
    {
        public static TurnSystem1 Instance { get; private set; }
        bool money = false, bench = false,iceCream = false;
        CharacterState currentState;
        CharacterState CurrentState
        {
            get => currentState;
            set
            {
                if (value < CharacterState.NORMAL)
                    SoundManager.Instance.PlaySound(SoundID.SFX_BORING);
                else if (value > CharacterState.NORMAL && value < CharacterState.LAUGH)
                    SoundManager.Instance.PlaySound(SoundID.SFX_HAPPY);
                else if (value == CharacterState.LAUGH)
                    SoundManager.Instance.PlaySound(SoundID.SFX_LAUGH);

                if (currentState == value)
                    return;
                
                ScoreManager.Instance.InitializeHappy((int)currentState * 0.2f);
                dict[currentState].SetActive(false);
                currentState = value;
            }
        }
        [SerializeField] NPCDict dict;
        [SerializeField] GameObject winPanel, losePanel;
        [SerializeField] GameObject creamMan,knifeMan,maskMan, normalGirl, benchGirl, girlConfession;
        [SerializeField] GameObject[] balloons;
        [SerializeField] GameObject benchObj,rain;
        [SerializeField] float timeGap = 1;

        private void Awake()
        {
            Instance = this;

            
            SoundManager.Instance.PlaySound(SoundID.MUSIC_GAMEPLAY);
            
        }

        private void Start()
        {
            SetUp(CharacterState.LITTLE_SAD);
            ScoreManager.Instance.InitializeHappy(0.4f);
            DialogueManager.Instance.Hide();
            CardManager.Instance.Hide();
            StartCoroutine(Cor_Init());
        }

        IEnumerator Cor_Init()
        {
            yield return new WaitForSeconds(2);
            DialogueManager.Instance.Show();
            DialogueManager.Instance.StartDialogue("A man feels bored because he is poor and no one loves him.\nSomeone needs a LAUGH here.", "");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue("It seems like everyone have their soulmate.", "THE MAN");
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
                case CardType.MONEY:
                    StartCoroutine(Cor_Money());
                    break;
                case CardType.CONFESSION:
                    StartCoroutine(Cor_Confession());
                    break;
                case CardType.BUY:
                    StartCoroutine(Cor_Buy());
                    break;
                case CardType.BENCH:
                    StartCoroutine(Cor_Bench());
                    break;
                case CardType.APPLE:
                    StartCoroutine(Cor_Apple());
                    break;
                case CardType.KNIFE:
                    StartCoroutine(Cor_Knife());
                    break;
                case CardType.BONE:
                    StartCoroutine(Cor_Bone());
                    break;
                case CardType.RICE:
                    StartCoroutine(Cor_Rice());
                    break;
                case CardType.BALLOON:
                    StartCoroutine(Cor_Balloon());
                    break;
                case CardType.SCARY_MASK:
                    StartCoroutine(Cor_ScaryMask());
                    break;
                case CardType.MILK:
                    StartCoroutine(Cor_Milk());
                    break;
                case CardType.RAINY:
                    StartCoroutine(Cor_Rainy());
                    break;
            }
        }

        void SetUp(CharacterState state)
        {
            currentState = state;
        }

        void DetectEndGame()
        {
            DialogueManager.Instance.Hide();
            if (currentState==CharacterState.VERY_SAD)
                losePanel.SetActive(true);
            else if (currentState==CharacterState.LAUGH)
                winPanel.SetActive(true);
            else
                CardManager.Instance.Show();
        }
        IEnumerator Cor_Money()
        {
            DialogueManager.Instance.StartDialogue( "You used a MONEY card.","");
            if (currentState < CharacterState.LITTLE_HAPPY)
                CurrentState++;
            dict[currentState].SetActive(true);
            money = true;
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "The man holds the money in his hand, feeling relieved.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);

            DetectEndGame();
        }

        IEnumerator Cor_Confession()
        {
            DialogueManager.Instance.StartDialogue( "You used a CONFESSION card.","");
            if (!bench)
            {
                CurrentState = CharacterState.VERY_SAD;
                normalGirl.SetActive(false);
                dict[currentState].SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "The man shows his love with the girl but her leg is aching so she doesn't care him.","");
            }    
            else
            {
                if (iceCream)
                {
                    benchGirl.SetActive(false);
                    girlConfession.SetActive(true);
                    CurrentState = CharacterState.LAUGH;
                    dict[currentState].SetActive(false);
                    creamMan.SetActive(true);
                    yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                    yield return new WaitForSeconds(timeGap);
                    DialogueManager.Instance.StartDialogue( "The man gives the girl an ice cream.\nShe accepts his love.\nThey have a happy conversation.","");
                }
                else
                {
                    benchGirl.SetActive(false);
                    CurrentState = CharacterState.VERY_SAD;
                    dict[currentState].SetActive(true);
                    yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                    yield return new WaitForSeconds(timeGap);
                    DialogueManager.Instance.StartDialogue( "The man shows his love with the girl but she has no feeling with him so she doesn't care him.","");
                }
            }
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Buy()
        {
            DialogueManager.Instance.StartDialogue( "You used a BUY card.","");
            if (!money)
            {
                CurrentState--;
                dict[currentState].SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "The man wants to buy something but he doesn't have enough money.","");
            }
            else
            {
                if (currentState < CharacterState.LITTLE_HAPPY)
                    CurrentState++;
                creamMan.SetActive(true);
                yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
                yield return new WaitForSeconds(timeGap);
                DialogueManager.Instance.StartDialogue( "The man decides to buy an ice cream at a cream car.","");
            }
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Bench()
        {
            DialogueManager.Instance.StartDialogue( "You used a BENCH card.","");
            bench = true;
            benchObj.SetActive(true);
            normalGirl.SetActive(false);
            benchGirl.SetActive(true);
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "The girl finds a bench in the park.\nShe takes a seat at the bench.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Apple()
        {
            DialogueManager.Instance.StartDialogue( "You used a APPLE card.","");
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Knife()
        {
            DialogueManager.Instance.StartDialogue( "You used a KNIFE card.","");
            CurrentState = (currentState > CharacterState.LITTLE_SAD) ? (currentState - 2) : CharacterState.VERY_SAD;
            iceCream = false;
            creamMan.SetActive(false);
            knifeMan.SetActive(true);
            maskMan.SetActive(false);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "People sees the man holding a knife.\nThey arrest him.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Bone()
        {
            DialogueManager.Instance.StartDialogue( "You used a BONE card.","");
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Rice()
        {
            DialogueManager.Instance.StartDialogue( "You used a RICE card.","");
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Balloon()
        {
            DialogueManager.Instance.StartDialogue( "You used a BALLOON card.","");
            if (currentState < CharacterState.LITTLE_HAPPY)
                CurrentState++;
            foreach (GameObject balloon in balloons)
                balloon.SetActive(true);
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "The park is decorated with balloons, making people feel excited.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_ScaryMask()
        {
            DialogueManager.Instance.StartDialogue( "You used a SCARY MASK card.","");
            iceCream = false;
            CurrentState--;
            creamMan.SetActive(false);
            knifeMan.SetActive(false);
            maskMan.SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "The mask makes eveyone scared.\nThey avoid him.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Milk()
        {
            DialogueManager.Instance.StartDialogue( "You used a MILK card.","");
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "Nothing happened.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }

        IEnumerator Cor_Rainy()
        {
            DialogueManager.Instance.StartDialogue( "You used a RAINY card.","");
            CurrentState = CharacterState.VERY_SAD;
            rain.SetActive(true);
            creamMan.SetActive(false);
            dict[currentState].SetActive(true);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DialogueManager.Instance.StartDialogue( "The rain falls.\nEveryone runs toward their home.\nThe man feel VERY SAD.","");
            yield return new WaitUntil(() => !DialogueManager.Instance.IsDialogActive);
            yield return new WaitForSeconds(timeGap);
            DetectEndGame();
        }
    }
}