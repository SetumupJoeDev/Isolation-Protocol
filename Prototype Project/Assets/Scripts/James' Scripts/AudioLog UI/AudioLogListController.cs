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
        //Loops through and generates a new button from the template for the number of entried in the list
        for( int i = 0; i < m_textTranscripts.Length; i++ )
        {
            //Instantiates a new button using the template button
            GameObject newButton = Instantiate(m_buttonTemplate);

            //Sets the button as active, so it can be set up correctly
            newButton.SetActive( true );

            //Creates a local variable and saves the AudiLogButton component of the new button to it for later use
            AudioLogButton buttonScript = newButton.GetComponent<AudioLogButton>( );

            //Sets the new buttons label, so that the player can tell each one apart in the list
            buttonScript.SetButtonLabelText( "Audio Log #" + ( i + 1 ) );

            //Sets the button's parent transform to be the same as that of the template, so it can be used in the scroll area mask
            newButton.transform.SetParent( m_buttonTemplate.transform.parent, false );

            //Sets the log transcript file of the new button to the matching one in the list
            buttonScript.SetLogTranscriptContent( m_textTranscripts[i] );

            //Sets the audio log clip of the new button to the matching one on the list
            buttonScript.SetButtonLogClip( m_audioLogClips[i] );

            //Sets the transcript UI text element of the new button so that, when clicked, a written version of the audiolog will display
            buttonScript.SetTranscriptTextElement( m_transcriptText.GetComponent<Text>( ) );

            //Adds the newly created button to the list so that they can later be unlocked sequentially
            m_audioLogButtons.Add( newButton );

            //Disables the button so that the player starts with no collected logs
            newButton.SetActive( false );

        }
    }

    public void UnlockNewLog( )
    {
        //Unlocks the next log in the list
        m_audioLogButtons[m_logsUnlocked].SetActive( true );

        //Increments the value of logsUnlocked so that the next log will be unlocked next time
        m_logsUnlocked++;

    }

}
