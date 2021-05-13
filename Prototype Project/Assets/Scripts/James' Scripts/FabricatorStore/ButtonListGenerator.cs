using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonListGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The template button that all generated buttons will be based off of.")]
    private GameObject m_buttonTemplate;

    [SerializeField]
    [Tooltip("The list of objects that each button in the list will be referencing.")]
    private FabricatorStoreProduct[] m_buttonObjectReferences;

    public List<GameObject> m_buttons;

    // Start is called before the first frame update
    void Start()
    {

        PopulateButtonList( );
        
    }

    public FabricatorStoreProduct GetStoreProduct(int itemIndex )
    {
        return m_buttonObjectReferences[itemIndex];
    }

    public void PopulateButtonList( )
    {
        for(int i = 0; i < m_buttonObjectReferences.Length; i++ )
        {
            GameObject newButton = Instantiate(m_buttonTemplate);

            newButton.SetActive( true );

            newButton.transform.SetParent( m_buttonTemplate.transform.parent, false );

            FabricatorProductButton newButtonController = newButton.GetComponent<FabricatorProductButton>();

            newButtonController.SetItemIndex( i );

            newButtonController.SetName( m_buttonObjectReferences[i].GetItemName( ) );

            newButtonController.SetImageSprite( m_buttonObjectReferences[i].GetItemSprite( ) );

            newButtonController.SetItemPrice( m_buttonObjectReferences[i].GetItemPrice( ) );

            newButtonController.SetItemDescription( m_buttonObjectReferences[i].GetItemDescription( ) );

            m_buttons.Add( newButton );

        }
    }

}
