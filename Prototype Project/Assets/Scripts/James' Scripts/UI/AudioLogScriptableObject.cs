using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLog", menuName = "ScriptableObjects/AudioLog", order = 1)]
public class AudioLogScriptableObject : ScriptableObject
{

    public AudioClip m_logAudio;

    public TextAsset m_logTranscript;

}
