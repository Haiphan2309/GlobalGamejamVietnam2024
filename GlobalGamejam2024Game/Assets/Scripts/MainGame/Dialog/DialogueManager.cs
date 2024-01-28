using System;
using UnityEngine;
using UnityUtilities;

namespace MainGame.Dialog
{
    public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
    {
        
        [SerializeField] private DialogueView _dialogueView;

        public bool IsDialogActive { get; set; } = true;
        
        public Action OnDialogueEnd;

        public void StartDialogue(string dialog, string speakerName, bool endLastDialogue = true, Action onDialogueEnd = null)
        {
            StartDialogue(new DialogueSentence(dialog, speakerName), endLastDialogue, onDialogueEnd );
        }
        public void StartDialogue(DialogueSentence dialogueSentence, bool endLastDialogue = true, Action onDialogueEnd = null)
        {
            if (!_dialogueView.gameObject.activeSelf) 
                _dialogueView.gameObject.SetActive(true);
            
            if (endLastDialogue)
                _dialogueView.ForceEndDialogue();
            
            _dialogueView.AddDialogue(dialogueSentence);
            
            
            OnDialogueEnd = onDialogueEnd;
            
            Show();
            
        }
        
        public void StartDialogue(DialogueSentence [] dialogueSentences, bool endLastDialogue = true,  Action onDialogueEnd = null)
        {
            if (!_dialogueView.gameObject.activeSelf) 
                _dialogueView.gameObject.SetActive(true);
            
            
            if (endLastDialogue)
                _dialogueView.ForceEndDialogue();
            
            foreach (var dialogueSentence in dialogueSentences)
            {
                _dialogueView.AddDialogue(dialogueSentence);
            }
            
            OnDialogueEnd = onDialogueEnd;
            
            Show();
        }
        
        
        public void EndDialogue()
        {
            IsDialogActive = false;
            
            OnDialogueEnd?.Invoke();
            
            Hide();
            
            
        }

        public void Show()
        {
            
            IsDialogActive = true;

            _dialogueView.Show();
        }
        
        public void Hide()
        {
            IsDialogActive = false;
            
            _dialogueView.Hide();
        }

    }
}