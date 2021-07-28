using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeListGenerator : ButtonListGenerator
{

    public override void PopulateButtonList( )
    {

        //Loops through for the length of product references and creates a new button for each
        for ( int i = 0; i < m_buttonObjectReferences.Length; i++ )
        {

            if ( m_buttonObjectReferences[i].GetIsUnlocked( ) )
            {

                //Instatiates a new button based on the button template
                GameObject newButton = Instantiate(m_buttonTemplate);

                //Activates the button so that it can be sut up and modified
                newButton.SetActive( true );

                //Sets the transform parent of the new button to be the same as the template, so it uses the scroll area
                newButton.transform.SetParent( m_buttonTemplate.transform.parent , false );

                SetupNewButton( newButton , i );

                //Adds the button to the list so it can be referenced by index later
                m_buttons.Add( newButton );

            }

        }

    }

}
