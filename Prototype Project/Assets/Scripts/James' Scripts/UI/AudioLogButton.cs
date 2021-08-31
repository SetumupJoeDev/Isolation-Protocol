using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class AudioLogButton : MonoBehaviour
{

    [Tooltip("The text file containing the audi log's transcript.")]
    public TextAsset m_logTranscriptContent;

    [Tooltip("The audio clip of this button's audio log.")]
    public AudioClip m_audioLogClip;

    [Tooltip("The text asset that display's the name of this audiolog.")]
    public Text m_buttonLabel;

    [Tooltip("The text element that contains and displays the text transcript of this audiolog.")]
    public TextMeshProUGUI m_transcriptText;

    [Tooltip("The AudioSource that plays the audio log clips.")]
    public AudioSource m_audioLogPlayer;

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

    public void OnClick( )
    {
        //Sets the transcript text object's content to the content of the text file
        m_transcriptText.text = m_logTranscriptContent.text;

        //Sets the sound clip of the audiolog audiosource to this button's clip
        m_audioLogPlayer.clip = m_audioLogClip;

    }

}
