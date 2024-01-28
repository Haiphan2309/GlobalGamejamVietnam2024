using System;
using MainGame.Dialog;
using UnityEngine;
using UnityUtilities;

namespace MainGame
{
    public class CardManager : SingletonMonoBehaviour<CardManager>
    {
        [SerializeField] private SO_Card [] _soCards;
        [SerializeField] private SO_NPC _soNpc;

        [SerializeField] private CardHandController cardHandController;
        
        
        private void Awake()
        {

            LoadCards();
        }
        
        private void LoadCards()
        {
            foreach (var cardSo in _soCards)
            {
                cardHandController.AddCard(cardSo);
            }
            
            cardHandController.Show();
            
        }

        

        
        //public void UseCard(CardController cardController, SO_Card cardSo)
        //{
            
        //    var dialogueSentence = new DialogueSentence(cardSo.Dialogue, _soNpc.Name);
        //    DialogueManager.Instance.StartDialogue(dialogueSentence, true);
            
        //    cardHandController.Hide();
            
        //}

        public void EndDialogue()
        {
            
            
            DialogueManager.Instance.ForceEndDialogue(); 
            
            cardHandController.Show();
        }
    }

}