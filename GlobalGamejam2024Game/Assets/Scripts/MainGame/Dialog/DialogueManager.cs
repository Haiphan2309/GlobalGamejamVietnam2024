﻿using UnityEngine;
using UnityUtilities;

namespace MainGame.Dialog
{
    public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
    {
        
        [SerializeField] private DialogueView _dialogueView;
        
        
        
        public void StartDialogue(string dialog, string speakerName, bool endLastDialogue = true)
        {
            StartDialogue(new DialogueSentence(dialog, speakerName), endLastDialogue );
        }
        public void StartDialogue(DialogueSentence dialogueSentence, bool endLastDialogue = true)
        {
            if (!_dialogueView.gameObject.activeSelf) 
                _dialogueView.gameObject.SetActive(true);
            
            if (endLastDialogue)
                _dialogueView.ForceEndDialogue();
            
            _dialogueView.AddDialogue(dialogueSentence);
        }
        
        public void StartDialogue(DialogueSentence [] dialogueSentences, bool endLastDialogue = true)
        {
            if (!_dialogueView.gameObject.activeSelf) 
                _dialogueView.gameObject.SetActive(true);
            
            
            if (endLastDialogue)
                _dialogueView.ForceEndDialogue();
            
            foreach (var dialogueSentence in dialogueSentences)
            {
                _dialogueView.AddDialogue(dialogueSentence);
            }
            
        }
        
        public void ForceEndDialogue()
        {
            _dialogueView.gameObject.SetActive(false);
            
        }


    }
}