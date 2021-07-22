using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricatorUpgradeListGenerator : ButtonListGenerator
{

    public FabricatorProductButton m_activeButton;

    public override void PopulateButtonList( )
    {
        base.PopulateButtonList( );
    }

    public override void SetupNewButton( GameObject newButton, int newButtonIndex )
    {
        //Saves the button's script as a local variable for later use
        FabricatorProductButton newButtonController = newButton.GetComponent<FabricatorProductButton>();

        newButtonController.m_buttonList = this;

        newButtonController.m_storeProduct = m_buttonObjectReferences[newButtonIndex];

        //Sets the item index of the button so it can be used to control the storefront
        newButtonController.SetItemIndex( newButtonIndex );

        //Sets the name of the button to reflect the product it represents
        newButtonController.SetName( m_buttonObjectReferences[newButtonIndex].GetItemName( ) );

        //Sets the sprite of the button to reflect the product it represents
        newButtonController.SetImageSprite( m_buttonObjectReferences[newButtonIndex].GetItemSprite( ) );

        //Sets the price of the button to reflect the product it represents
        newButtonController.SetItemPrice( m_buttonObjectReferences[newButtonIndex].GetItemPrice( ) );

        //Sets the description of the button to reflect the product it represents
        newButtonController.SetItemDescription( m_buttonObjectReferences[newButtonIndex].GetItemDescription( ) );
    }

}
