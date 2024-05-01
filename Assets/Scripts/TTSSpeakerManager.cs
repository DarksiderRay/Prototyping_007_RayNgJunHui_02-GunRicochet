using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using NaughtyAttributes;
using UnityEngine;

public class TTSSpeakerManager : MonoBehaviour
{
    [SerializeField] private TTSSpeaker speaker;
    [SerializeField] private string text;

    [Button]
    private void Speak()
    {
        speaker.Speak(text);
    }
}
