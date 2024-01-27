using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace MainGame.Dialog
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private TMP_Text _speakerNameText;
        
        [SerializeField] private float _textSpeed = 0.1f;
        [SerializeField] private float _textDelay = 0.5f;
        
        [SerializeField] private int _characterSkip = 0;
        
        public Action OnDialogueEnd;
        public Action OnDialogueStart;
        public Action OnCharacterDisplay;
        
        private Coroutine _typeSentenceCoroutine;
        
        public void StartDialogue( string speakerName, string dialogue)
        {
            _speakerNameText.text = speakerName;
            _dialogueText.text = "";
            
            
            OnDialogueStart?.Invoke();
            
            
            _typeSentenceCoroutine = StartCoroutine(TypeSentence(dialogue));            
            
        }
        
        public void EndDialogue()
        {
            
            OnDialogueEnd?.Invoke();
            
            if (_typeSentenceCoroutine != null)
            {
                StopCoroutine(_typeSentenceCoroutine);
            }
            
            _dialogueText.text = "";
            _speakerNameText.text = "";
            
        }
        
        
        private IEnumerator TypeSentence(string sentence)
        {
            
            yield return new WaitForSeconds(_textDelay);
            
            int characterCount = 0;
            
            foreach (char letter in sentence.ToCharArray())
            {
                _dialogueText.text += letter;
        
                characterCount++;
                
                if (characterCount >= _characterSkip)
                {
                    OnCharacterDisplay?.Invoke();
                    characterCount = 0;
                }
                
                yield return new WaitForSeconds(_textSpeed);
            }
            
        }
        
        
        
    }
}