using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> Variables { get; private set; }

    private readonly Story globalVariablesStory;
    private const string SAVE_VARIABLES_KEY = "INK_VARIABLES";

    public DialogueVariables(TextAsset loadGlobalsJson) 
    {
        // create the story
        globalVariablesStory = new Story(loadGlobalsJson.text);
        // if we have saved data, load it
        // if (PlayerPrefs.HasKey(saveVariablesKey))
        // {
        //     string jsonState = PlayerPrefs.GetString(saveVariablesKey);
        //     globalVariablesStory.state.LoadJson(jsonState);
        // }

        // initialize the dictionary
        Variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            Variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void SaveVariables() 
    {
        if (globalVariablesStory != null) 
        {
            // Load the current state of all of our variables to the globals story
            VariablesToStory(globalVariablesStory);
            // NOTE: eventually, you'd want to replace this with an actual save/load method
            // rather than using PlayerPrefs.
            PlayerPrefs.SetString(SAVE_VARIABLES_KEY, globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story) 
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) 
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value) 
    {
        // only maintain variables that were initialized from the globals ink file
        if (Variables.ContainsKey(name)) 
        {
            Variables.Remove(name);
            Variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story) 
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in Variables) 
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

}