using System;
using MainGame.Dialog;
using UnityEngine;
using UnityUtilities;

namespace MainGame
{
    public class GameLoopManager : SingletonMonoBehaviour<GameLoopManager>
    {
        [SerializeField] private SO_Card [] _soCards;
        [SerializeField] private SO_NPC _soNpc;

        [SerializeField] private CardHandSystem cardHandSystem;
        
        
        private void Awake()
        {

            LoadCards();
        }
        
        private void LoadCards()
        {
            foreach (var cardSo in _soCards)
            {
                cardHandSystem.AddCard(cardSo);
            }
            
            cardHandSystem.Show();
            
        }

        

        
        //public void UseCard(CardController cardController, SO_Card cardSo)
        //{
            
        //    var dialogueSentence = new DialogueSentence(cardSo.Dialogue, _soNpc.Name);
        //    DialogueManager.Instance.StartDialogue(dialogueSentence, true);
            
        //    cardHandSystem.Hide();
            
        //}

        public void EndDialogue()
        {
            
            
            DialogueManager.Instance.ForceEndDialogue(); 
            
            cardHandSystem.Show();
        }
    }

}