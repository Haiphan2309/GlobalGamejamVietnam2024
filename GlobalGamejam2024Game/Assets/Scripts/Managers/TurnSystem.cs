using GDC.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level_1
{

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
                switch(value)
                {
                    case CharacterState.VERY_SAD:
                        {
                            //player anim very sad (maybe suicide)
                            //lose
                        }
                        break;
                    case CharacterState.SAD:
                        {
                            //player anim sad
                        }
                        break;
                    case CharacterState.LITTLE_SAD:
                        {
                            //player anim little sad
                        }
                        break;
                    case CharacterState.NORMAL:
                        {
                            //player anim normal
                        }
                        break;
                    case CharacterState.LITTLE_HAPPY:
                        {
                            //player anim little happy
                        }
                        break;
                    case CharacterState.LAUGH:
                        {
                            //player anim laugh
                            //victory
                        }
                        break;
                }
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void EndTurn(CardType type)//call when a card is clicked
        {
            switch (type)
            {
                case CardType.CAT_PICTURE:
                    break;
            }
        }

        void CatPicture()
        {
            if (currentState < CharacterState.LITTLE_HAPPY) 
                currentState++;
            //dialogue You used a cat picture card
            switch (currentState)
            {
                case CharacterState.LITTLE_SAD:
                    //dialogue The man look in the picture hang on the wall and think some thing
                    break;

            }
        }
    }
}

