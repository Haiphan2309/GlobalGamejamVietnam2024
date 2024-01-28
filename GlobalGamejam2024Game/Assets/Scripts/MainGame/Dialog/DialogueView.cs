using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        
        [SerializeField] private RectTransform _speakerNameView;
        
        [SerializeField] private float _textSpeed = 0.1f;
        [SerializeField] private float _textDelay = 0.5f;
        
        [SerializeField] private int _characterSkip = 0;
        
        [SerializeField] private RectTransform _dialogueView;
        [SerializeField] private RectTransform _showTransform;
        [SerializeField] private RectTransform _hideTransform;
        
        [SerializeField] private Ease _moveEase = Ease.OutCubic;
        [SerializeField] private float _moveDuration = 0.25f;
        
        
        
        public Action OnDialogueEnd;
        public Action OnDialogueStart;
        public Action OnCharacterDisplay;
        
        private Coroutine _typeSentenceCoroutine;
        private bool _isSkipDialogue = false;
        private bool _isRunning = false;
        
        private Queue<DialogueSentence> _dialogueQueue = new();
        
        private Tween _moveTween;
        
        private void Awake()
        {
            _dialogueText.text = "";
            _speakerNameText.text = "";
            
            _nextButton.onClick.AddListener(NextDialogue);
            
        }
        
        
        
        public void AddDialogue(DialogueSentence dialogueSentence)
        {
            _dialogueQueue.Enqueue(dialogueSentence);
            
            if (!_isRunning)
            {
                StartDialogue();
            }
        }
        
        
        public void StartDialogue()
        {
            _speakerNameText.text = "";
            _dialogueText.text = "";
            
            _isSkipDialogue = false;
            
            OnDialogueStart?.Invoke();
            
            
            _typeSentenceCoroutine = StartCoroutine(nameof(TypeSentence));            
            
        }

        private void NextDialogue()
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
            
            
            DialogueManager.Instance.EndDialogue();
        }
        
        public void ForceEndDialogue()
        {
            if (_typeSentenceCoroutine != null)
            {
                StopCoroutine(_typeSentenceCoroutine);
            }
            
            _dialogueText.text = "";
            _speakerNameText.text = "";
            
            OnDialogueEnd?.Invoke();
            
        }
        
        public void Show()
        {
            _moveTween?.Kill();
            _dialogueView.gameObject.SetActive(true);
            _moveTween = _dialogueView.DOAnchorPos(_showTransform.anchoredPosition, _moveDuration).SetEase(_moveEase);
        }

        public void Hide()
        {
            _moveTween?.Kill();
            _moveTween = _dialogueView.DOAnchorPos(_hideTransform.anchoredPosition, _moveDuration).SetEase(_moveEase)
                .OnComplete(() => _dialogueView.gameObject.SetActive(false)
                );
        }
        
        
        private IEnumerator TypeSentence()
        {
            
            yield return new WaitForSeconds(_textDelay);
            
            int characterCount = 0;
            while (_dialogueQueue.Count > 0)
            {
                _isSkipDialogue = false;
                var dialogueSentence = _dialogueQueue.Dequeue();
                
                
                SetSpeakerName(dialogueSentence.SpeakerName);
                foreach (char letter in dialogueSentence.Dialogue)
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
        
        
        private void SetSpeakerName(string speakerName)
        {
            if (string.IsNullOrEmpty(speakerName))
            {
                _speakerNameView.gameObject.SetActive(false);
                _speakerNameText.text = speakerName;
            }
            else
            {
                _speakerNameView.gameObject.SetActive(true);
                _speakerNameText.text = speakerName;
            }
        }
        
        
    }
}