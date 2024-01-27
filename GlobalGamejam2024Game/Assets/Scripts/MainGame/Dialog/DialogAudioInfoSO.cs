using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoDescription", order = 1)]
public class DialogueAudioInfoDescription : ScriptableObject
{
    public string ID;
    public AudioClip[] DialogueTypingSoundClips;
    [Range(1, 5)]
    public int FrequencyLevel = 2;
    [Range(-3, 3)]
    public float MinPitch = 0.5f;
    [Range(-3, 3)]
    public float MaxPitch = 3f;
    public bool StopAudioSource;
}