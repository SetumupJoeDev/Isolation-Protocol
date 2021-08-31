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

        //Sets the name of the button to reflect the product it represents
        newButtonController.m_productName.text = m_buttonObjectReferences[newButtonIndex].m_itemName;

        //Sets the sprite of the button to reflect the product it represents
        newButtonController.m_productImage.sprite = m_buttonObjectReferences[newButtonIndex].m_itemSprite;

        //Sets the price of the button to reflect the product it represents
        newButtonController.SetItemPrice( m_buttonObjectReferences[newButtonIndex].GetItemPrice( ) );

        //Sets the description file of the button as the object reference's description file
        newButtonController.m_productDescriptionFile = m_buttonObjectReferences[newButtonIndex].m_itemDescription;

        //Sets the description of the button to reflect the product it represents
        newButtonController.m_itemDescriptionUI.text = m_buttonObjectReferences[newButtonIndex].m_itemDescription.text;
    }

}
