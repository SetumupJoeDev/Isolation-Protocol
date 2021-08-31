using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLog", menuName = "ScriptableObjects/AudioLog", order = 1)]
public class AudioLogScriptableObject : ScriptableObject
{

    [Tooltip("The audio clip associated with this audiolog.")]
    public AudioClip m_logAudio;

    [Tooltip("The text transcript associated with this audiolog.")]
    public TextAsset m_logTranscript;

}
