using GDC.Enums;
using Level_1;
using MainGame.Dialog;
using UnityEngine;

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
                if (currentState == value)
                    return;
                dict[currentState].SetActive(false);
                currentState = value;
                if (currentState == CharacterState.VERY_SAD)
                {
                    creamMan.SetActive(false);
                    losePanel.SetActive(true);
                }
                if (currentState == CharacterState.LAUGH)
                    winPanel.SetActive(true);
            }
        }
        [SerializeField] NPCDict dict;
        [SerializeField] GameObject winPanel, losePanel;
        [SerializeField] GameObject creamMan,normalGirl, benchGirl, girlConfession;
        [SerializeField] GameObject[] balloons;

        private void Awake()
        {
            Instance = this;
            SetUp(CharacterState.LITTLE_SAD);
        }

        public void PickUpCard(CardType type)//call when a card is clicked
        {
            switch (type)
            {
                case CardType.MONEY:
                    Money();
                    break;
                case CardType.CONFESSION:
                    Confession();
                    break;
                case CardType.BUY:
                    Buy();
                    break;
                case CardType.BENCH:
                    Bench();
                    break;
                case CardType.APPLE:
                    Apple();
                    break;
                case CardType.KNIFE:
                    Knife();
                    break;
                case CardType.BONE:
                    Bone();
                    break;
                case CardType.RICE:
                    Rice();
                    break;
                case CardType.BALLOON:
                    Balloon();
                    break;
                case CardType.SCARY_MASK:
                    ScaryMask();
                    break;
                case CardType.MILK:
                    Milk();
                    break;
                case CardType.RAINY:
                    Rainy();
                    break;
            }
        }

        void SetUp(CharacterState state)
        {
            currentState = state;
        }
        void Money()
        {
            DialogueManager.Instance.StartDialogue("", "You used a MONEY card.");
            if (currentState < CharacterState.LITTLE_HAPPY)
                CurrentState++;
            dict[currentState].SetActive(true);
            money = true;
            DialogueManager.Instance.StartDialogue("", "The man holds the money in his hand, feeling relieved.");
        }

        void Confession()
        {
            DialogueManager.Instance.StartDialogue("", "You used a CONFESSION card.");
            if (!bench)
            {
                CurrentState = CharacterState.VERY_SAD;
                normalGirl.SetActive(false);
                dict[currentState].SetActive(true);
                DialogueManager.Instance.StartDialogue("", "The man shows his love with the girl but her leg is aching so she doesn't care him.");
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
                    DialogueManager.Instance.StartDialogue("", "The man gives the girl an ice cream.\nShe accepts his love.\nThey have a happy conversation.");
                }
                else
                {
                    benchGirl.SetActive(false);
                    CurrentState = CharacterState.VERY_SAD;
                    dict[currentState].SetActive(true);
                    DialogueManager.Instance.StartDialogue("", "The man shows his love with the girl but she has no feeling with him so she doesn't care him.");
                }
            }
        }

        void Buy()
        {
            DialogueManager.Instance.StartDialogue("", "You used a BUY card.");
            if (!money)
            {
                CurrentState--;
                dict[currentState].SetActive(true);
                DialogueManager.Instance.StartDialogue("", "The man wants to buy something but he doesn't have enough money.");
            }
            else
            {
                if (currentState < CharacterState.LITTLE_HAPPY)
                    CurrentState++;
                creamMan.SetActive(true);
                DialogueManager.Instance.StartDialogue("", "The man decides to buy an ice cream at a cream car.");
            }
        }

        void Bench()
        {
            DialogueManager.Instance.StartDialogue("", "You used a BENCH card.");
            bench = true;
            normalGirl.SetActive(false);
            benchGirl.SetActive(true);
            DialogueManager.Instance.StartDialogue("", "The girl finds a bench in the park.\nShe takes a seat at the bench.");
        }

        void Apple()
        {
            DialogueManager.Instance.StartDialogue("", "You used a APPLE card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            dict[currentState].SetActive(true);
        }

        void Knife()
        {
            DialogueManager.Instance.StartDialogue("", "You used a KNIFE card.");
            CurrentState = (currentState > CharacterState.LITTLE_SAD) ? (currentState - 2) : CharacterState.VERY_SAD;
            iceCream = false;
            dict[currentState].SetActive(true);
            DialogueManager.Instance.StartDialogue("", "People sees the man holding a knife.\nThey arrest him.");
        }

        void Bone()
        {
            DialogueManager.Instance.StartDialogue("", "You used a BONE card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            dict[currentState].SetActive(true);
        }

        void Rice()
        {
            DialogueManager.Instance.StartDialogue("", "You used a RICE card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            dict[currentState].SetActive(true);
        }

        void Balloon()
        {
            DialogueManager.Instance.StartDialogue("", "You used a BALLOON card.");
            if (currentState < CharacterState.LITTLE_HAPPY)
                CurrentState++;
            foreach (GameObject balloon in balloons)
                balloon.SetActive(true);
            dict[currentState].SetActive(true);
            DialogueManager.Instance.StartDialogue("", "The park is decorated with balloons, making people feel excited.");
        }

        void ScaryMask()
        {
            DialogueManager.Instance.StartDialogue("", "You used a SCARY MASK card.");
            iceCream = false;
            currentState--;
            dict[currentState].SetActive(true);
            DialogueManager.Instance.StartDialogue("", "The mask makes eveyone scared.\nThey avoid him.");
        }

        void Milk()
        {
            DialogueManager.Instance.StartDialogue("", "You used a MILK card.");
            DialogueManager.Instance.StartDialogue("", "Nothing happened.");
            dict[currentState].SetActive(true);
        }

        void Rainy()
        {
            DialogueManager.Instance.StartDialogue("", "You used a RAINY card.");
            CurrentState = CharacterState.VERY_SAD;
            dict[currentState].SetActive(true);
            DialogueManager.Instance.StartDialogue("", "The rain falls.\nEveryone runs toward their home.\nThe man feel VERY SAD.");
        }
    }
}