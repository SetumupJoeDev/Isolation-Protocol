using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioLogButton : MonoBehaviour
{

    public TextAsset m_logTranscriptContent;

    public AudioClip m_audioLogClip;

    public Text m_buttonLabel;

    public Text m_transcriptText;

    public AudioSource m_audioLogPlayer;

    public void SetButtonLabelText( string labelText )
    {
        m_buttonLabel.text = labelText;
    }

    public void OnClick( )
    {
        m_transcriptText.text = m_logTranscriptContent.text;

        m_audioLogPlayer.clip = m_audioLogClip;

    }

}
