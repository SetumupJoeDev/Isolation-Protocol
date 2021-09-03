using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FabricatorProductButton : InterfaceButton
{

    [Tooltip("The script that controls the fabricator store.")]
    public FabricatorStoreBase m_storeController;

    [Tooltip("The button list generator that contains this button.")]
    public FabricatorUpgradeListGenerator m_buttonList;

    [Tooltip("The fabricator product that is purchased using this button.")]
    public FabricatorStoreProduct m_storeProduct;

    [Tooltip("A boolean that determines whether or not this button is the currently selected button.")]
    public bool m_isSelected;

    [Tooltip("A soundclip that plays when the player has insufficient funds to purchase the product this button represents.")]
    public AudioClip m_insufficientFundsClip;

    [Tooltip("The price of the product this button represents.")]
    public int m_productPrice;

    [Tooltip("The file that contains a description of the product this button represents.")]
    public TextAsset m_productDescriptionFile;

    [Tooltip("The UI text element that displays the product description on-screen.")]
    public TextMeshProUGUI m_itemDescriptionUI;

    [Tooltip("The UI text element that displays the product's price on-screen.")]
    public TextMeshProUGUI m_itemPriceText;

    //The text that is displayed on the button once the product has been bought
    private string m_outOfStockText = "Out Of Stock";

    public void SetItemPrice(int newPrice )
    {
        m_productPrice = newPrice;
        //If the product has not yet been bought, the price of the item is displayed on the button
        if ( !m_storeProduct.GetIsUnlocked( ) )
        {
            m_itemPriceText.text = m_productPrice.ToString();
        }
        //Otherwise, the product is listed as "Out of stock"
        else
        {
            m_itemPriceText.text = m_outOfStockText;

            m_itemPriceText.color = Color.red;

        }
    }

    public override void OnClick( )
    {
        //If this is not the currently active button, it is set as the new active button
        if ( m_buttonList.m_activeButton != this )
        {
            //If there is another button set as active, it is set as inactive now so that clicking it again won't purchase its product
            if ( m_buttonList.m_activeButton != null )
            {
                m_buttonList.m_activeButton.m_isSelected = false;
            }

            //This button is set as the new active button and the product information displayed on-screen is updated
            m_buttonList.m_activeButton = this;

            UpdateProductInfo( );

        }
        //Otherwise, if it is the active button, we check if the player can afford the product. If they can, the product is purchased
        else if ( m_storeController.PlayerCanAfford( m_productPrice ) )
        {
            //The item is set as unlocked so it can be accessed in the upgrade managers in the case of weapons and drone upgrades, or activated in the case of Exosuit upgrades
            m_storeProduct.SetIsUnlocked( true );

            //The player is charged using the price of the product
            m_storeController.ChargePlayer( m_productPrice );

            //Call the player upgrade unlock event to trigger changes in the player according to product name
            FabricatorEventListener.current.PlayerUpgradeUnlock(m_storeProduct.m_itemName);
            
            //Calls the fabricator unlock event to trigget a quicksave event
            FabricatorEventListener.current.FabricatorProductUnlock();
            
            //Update's the product infro to show it as "Out of stock"
            UpdateProductInfo( );
        }
        else
        {
            //Otherwise, if the player can't afford the product, it plays the insufficient funds clip
            AudioSource.PlayClipAtPoint(m_insufficientFundsClip, transform.position);
        }
    }

    public void UpdateProductInfo( )
    {
        //Sets the text of the itemDescription UI element to that contained within the button's product description file
        m_itemDescriptionUI.text = m_productDescriptionFile.text;

        if ( !m_storeProduct.GetIsUnlocked( ) )
        {
            //Sets the text of the buy button's price text element to the value stored in the button so the correct price is displayed to the player
            m_itemPriceText.text = m_productPrice.ToString( );
        }
        else
        {
            //Otherwise, the product is listed as "Out of stock"
            m_itemPriceText.text = m_outOfStockText;
        }
    }

}
