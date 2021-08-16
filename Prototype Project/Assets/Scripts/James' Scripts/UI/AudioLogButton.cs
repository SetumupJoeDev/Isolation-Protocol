using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioLogButton : MonoBehaviour
{

    [SerializeField]
    private TextAsset m_logTranscriptContent;

    [SerializeField]
    private AudioClip m_audioLogClip;

    [SerializeField]
    private Text m_buttonLabel;

    [SerializeField]
    private Text m_transcriptText;

    [SerializeField]
    private AudioSource m_audioLogPlayer;

    public void SetButtonLabelText( string labelText )
    {
        //Sets the label text of the button to the value passed in
        m_buttonLabel.text = labelText;
    }

    public void SetLogTranscriptContent( TextAsset newFile )
    {
        m_logTranscriptContent = newFile;
    }

    public void SetButtonLogClip( AudioClip newClip )
    {
        m_audioLogClip = newClip;
    }

    public void SetTranscriptTextElement( Text newTextElement )
    {
        m_transcriptText = newTextElement;
    }

    public void OnClick( )
    {
        //Sets the transcript text object's content to the content of the text file
        m_transcriptText.text = m_logTranscriptContent.text;

        //Sets the sound clip of the audiolog audiosource to this button's clip
        m_audioLogPlayer.clip = m_audioLogClip;

    }

}
