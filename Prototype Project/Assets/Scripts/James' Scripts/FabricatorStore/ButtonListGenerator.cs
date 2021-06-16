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

    public void PopulateButtonList( )
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

            //Saves the button's script as a local variable for later use
            FabricatorProductButton newButtonController = newButton.GetComponent<FabricatorProductButton>();

            //Sets the item index of the button so it can be used to control the storefront
            newButtonController.SetItemIndex( i );

            //Sets the name of the button to reflect the product it represents
            newButtonController.SetName( m_buttonObjectReferences[i].GetItemName( ) );

            //Sets the sprite of the button to reflect the product it represents
            newButtonController.SetImageSprite( m_buttonObjectReferences[i].GetItemSprite( ) );

            //Sets the price of the button to reflect the product it represents
            newButtonController.SetItemPrice( m_buttonObjectReferences[i].GetItemPrice( ) );

            //Sets the description of the button to reflect the product it represents
            newButtonController.SetItemDescription( m_buttonObjectReferences[i].GetItemDescription( ) );

            //Adds the button to the list so it can be referenced by index later
            m_buttons.Add( newButton );

        }
    }

}
