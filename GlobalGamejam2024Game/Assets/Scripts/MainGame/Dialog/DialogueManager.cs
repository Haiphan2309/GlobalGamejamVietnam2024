using UnityEngine;
using UnityUtilities;

namespace MainGame.Dialog
{
    public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
    {
        
        [SerializeField] private DialogueView _dialogueView;
        
        public void StartDialogue(string speakerName, string dialogue)
        {
            _dialogueView.gameObject.SetActive(true);
            _dialogueView.StartDialogue(speakerName, dialogue);
        }
        
        public void EndDialogue()
        {
            _dialogueView.gameObject.SetActive(false);
            _dialogueView.EndDialogue();
        }
        
        
    }
}