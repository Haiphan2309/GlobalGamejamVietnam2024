using MainGame.Dialog;
using UnityEngine;

namespace MainGame.Mock
{
    public class DialogueStoryExample : MonoBehaviour
    {
        [SerializeField] private DialogueSentence[] _dialogueSentences;
        private void Start()
        {
            DialogueManager.Instance.StartDialogue(_dialogueSentences, true);
        }
        
        
    }
}