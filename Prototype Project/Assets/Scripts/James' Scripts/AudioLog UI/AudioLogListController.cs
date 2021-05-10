using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioLogListController : MonoBehaviour
{

    public TextAsset[] m_textTranscripts;

    public AudioClip[] m_audioLogClips;

    public GameObject m_transcriptText;

    public List<GameObject> m_audioLogButtons;

    public GameObject m_buttonTemplate;

    public int m_logsUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        PopulateButtonList( );
    }

    
    public void PopulateButtonList( )
    {
        for( int i = 0; i < m_textTranscripts.Length; i++ )
        {
            GameObject newButton = Instantiate(m_buttonTemplate);

            newButton.SetActive( true );

            AudioLogButton buttonScript = newButton.GetComponent<AudioLogButton>( );

            buttonScript.SetButtonLabelText( "Audio Log #" + ( i + 1 ) );

            newButton.transform.SetParent( m_buttonTemplate.transform.parent, false );

            buttonScript.m_logTranscriptContent = m_textTranscripts[i];

            buttonScript.m_audioLogClip = m_audioLogClips[i];

            buttonScript.m_transcriptText = m_transcriptText.GetComponent<Text>( );

            m_audioLogButtons.Add( newButton );

            newButton.SetActive( false );

        }
    }

    public void UnlockNewLog( )
    {

        m_audioLogButtons[m_logsUnlocked].SetActive( true );

        m_logsUnlocked++;

    }

}
