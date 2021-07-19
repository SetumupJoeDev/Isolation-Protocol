using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonListGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The template button that all generated buttons will be based off of.")]
    private GameObject m_buttonTemplate;

    [Tooltip("The list of objects that each button in the list will be referencing.")]
    public FabricatorStoreProduct[] m_buttonObjectReferences;

    public List<GameObject> m_buttons;

    // Start is called before the first frame update
    void Start()
    {
        //Populates the list of buttons on startup
        PopulateButtonList( );
        
    }

    public FabricatorStoreProduct GetStoreProduct( int itemIndex )
    {
        //Returns the product in the list at the index passed in to the method
        return m_buttonObjectReferences[itemIndex];
    }

    public virtual void PopulateButtonList( )
    {
        //Loops through for the length of product references and creates a new button for each
        for(int i = 0; i < m_buttonObjectReferences.Length; i++ )
        {

            //Instatiates a new button based on the button template
            GameObject newButton = Instantiate(m_buttonTemplate);

            //Activates the button so that it can be sut up and modified
            newButton.SetActive( true );

            //Sets the transform parent of the new button to be the same as the template, so it uses the scroll area
            newButton.transform.SetParent( m_buttonTemplate.transform.parent, false );

            SetupNewButton( newButton, i );

            //Adds the button to the list so it can be referenced by index later
            m_buttons.Add( newButton );

        }
    }

    public virtual void SetupNewButton( GameObject newButton, int newButtonIndex )
    {
        //Set up the different necessary variables of the button here, ie name text, image, object references etc.

        InterfaceButton newButtonController = newButton.GetComponent<InterfaceButton>();

        newButtonController.m_productName.text = m_buttonObjectReferences[newButtonIndex].GetItemName( );

        newButtonController.m_productImage.sprite = m_buttonObjectReferences[newButtonIndex].GetItemSprite( );

    }

}
