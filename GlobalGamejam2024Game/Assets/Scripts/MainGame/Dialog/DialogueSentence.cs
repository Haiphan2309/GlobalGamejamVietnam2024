
using System;

[Serializable]
public struct DialogueSentence
{
    
    public string Dialogue;
    public string SpeakerName;

    public DialogueSentence(string dialogue, string speakerName = "" )
    {
        SpeakerName = speakerName;
        Dialogue = dialogue;
    }
    
}

