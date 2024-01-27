using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJson;

    private bool _playerInRange;

    private void Awake() 
    {
        _playerInRange = false;
        _visualCue.SetActive(false);
    }

    private void Update() 
    {
        if (_playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            _visualCue.SetActive(true);
            if (DialogInputManager.Instance.GetSubmitPressed()) // TODO: Change this to a button press 
            {
                DialogueManager.GetInstance().EnterDialogueMode(_inkJson);
            }
        }
        else 
        {
            _visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}