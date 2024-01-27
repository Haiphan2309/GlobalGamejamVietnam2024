using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame.Dialog
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private TMP_Text _speakerNameText;
        [SerializeField] private Button _nextButton;
        
        [SerializeField] private float _textSpeed = 0.1f;
        [SerializeField] private float _textDelay = 0.5f;
        
        [SerializeField] private int _characterSkip = 0;
        
        public Action OnDialogueEnd;
        public Action OnDialogueStart;
        public Action OnCharacterDisplay;
        
        private Coroutine _typeSentenceCoroutine;
        private bool _isSkipDialogue = false;
        
        
        private void Awake()
        {
            _dialogueText.text = "";
            _speakerNameText.text = "";
            
            _nextButton.onClick.AddListener(EndDialogue);
            
        }
        
        
        public void StartDialogue( string speakerName, string dialogue)
        {
            _speakerNameText.text = speakerName;
            _dialogueText.text = "";
            
            _isSkipDialogue = false;
            
            OnDialogueStart?.Invoke();
            
            
            _typeSentenceCoroutine = StartCoroutine(TypeSentence(dialogue));            
            
        }
        
        public void EndDialogue()
        {
            if (!_isSkipDialogue)
            {
                _isSkipDialogue = true;
                return;
            }
            
            OnDialogueEnd?.Invoke();
            
            if (_typeSentenceCoroutine != null)
            {
                StopCoroutine(_typeSentenceCoroutine);
            }
            
            _dialogueText.text = "";
            _speakerNameText.text = "";
            
            
            GameLoopManager.Instance.EndDialogue();
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
                
                if (!_isSkipDialogue) 
                    yield return new WaitForSeconds(_textSpeed);
            }
            
            _isSkipDialogue = true;
            
        }
        
        
        
    }
}