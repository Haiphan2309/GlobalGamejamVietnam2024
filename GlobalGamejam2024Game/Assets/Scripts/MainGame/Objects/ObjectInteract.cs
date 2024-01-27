using DG.Tweening;
using MainGame.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] private DialogueSentence[] _dialogueSentences;
    
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
        
        DialogueManager.Instance.StartDialogue( _dialogueSentences, true);
    }
}
