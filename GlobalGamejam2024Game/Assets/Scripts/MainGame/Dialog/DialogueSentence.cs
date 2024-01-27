
public struct DialogueSentence
{
    
    public readonly string Dialogue;
    public readonly string SpeakerName;

    public DialogueSentence(string dialogue, string speakerName = "" )
    {
        SpeakerName = speakerName;
        Dialogue = dialogue;
    }
    
}

