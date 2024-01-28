using DG.Tweening;
using MainGame.Dialog;
using System.Collections;
using System.Collections.Generic;
using MainGame;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] private DialogueSentence[] _dialogueSentences;
    [SerializeField] SpriteRenderer _spriteRenderer;
    
    private void OnMouseEnter()
    {
        DOTween.Kill(transform);
        transform.DOScale(1.2f, 0.15f).OnComplete(() =>
        {
            transform.DOScale(1, 0.15f);
        });
    }

    private void OnMouseDown()
    {
        DOTween.Kill(transform);
        transform.DOScale(0.8f, 0.15f).OnComplete(() =>
        {
            transform.DOScale(1, 0.15f);
        });
        
        CheckDialogue();
    }
    
    
    private void CheckDialogue()
    {
        if (!DialogueManager.Instance.IsDialogActive && DialogueManager.Instance.IsEnabled)
        {
            CardManager.Instance.Hide();
            DialogueManager.Instance.StartDialogue(_dialogueSentences, false, () =>
            {
                CardManager.Instance.Show();
            });
        }
    }

    private void OnEnable()
    {
        Color fadeCorlor = Color.white;
        fadeCorlor.a = 0;
        _spriteRenderer.color = fadeCorlor;
        _spriteRenderer.DOFade(1, 0.3f).SetLoops(3, LoopType.Yoyo);
    }
}
