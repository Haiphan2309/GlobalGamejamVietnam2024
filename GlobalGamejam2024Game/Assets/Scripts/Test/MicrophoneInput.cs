using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MicrophoneInput : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private readonly Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Add your laugh detection keyword(s) here
    public string[] laughKeywords = {
        "Ha",
        "Hi",
        "Haha",
        "Forward"
    };

    void Start()
    {
        foreach (var laughKeyword in laughKeywords)
        {
            keywords.Add(laughKeyword, () => {
                // Call your laugh function or perform actions when laugh is recognized
                Debug.Log("Laugh recognized! " + laughKeyword);
            });
        }

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();

        StartCoroutine(InitializeMicrophone());
    }

    IEnumerator InitializeMicrophone()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

        if (Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            string[] devices = Microphone.devices;
            if (devices.Length > 0)
            {
                string deviceName = devices[0];
                Debug.Log("Microphone initialized: " + deviceName);
                Microphone.Start(deviceName, true, 5, AudioSettings.outputSampleRate);
            }
            else
            {
                Debug.LogError("No microphone devices found.");
            }
        }
        else
        {
            Debug.LogError("Microphone access not granted by the user.");
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (keywords.TryGetValue(args.text, out var keywordAction))
        {
            keywordAction.Invoke();
        }
    }


    private void OnApplicationQuit()
    {
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }
        
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
        }
        
        
    }
}
