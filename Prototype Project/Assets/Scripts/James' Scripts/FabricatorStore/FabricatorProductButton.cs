using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FabricatorProductButton : MonoBehaviour
{

    [SerializeField]
    private FabricatorStoreBase m_storeController;

    [SerializeField]
    private Image m_productImage;

    [SerializeField]
    private Text m_productName;

    [SerializeField]
    private int m_productPrice;

    private int m_productIndex;

    [SerializeField]
    private TextAsset m_productDescriptionFile;

    [SerializeField]
    private Text m_itemDescriptionUI;

    [SerializeField]
    private Text m_itemPriceText;

    public void SetName(string newName )
    {
        m_productName.text = newName;
    }

    public void SetImageSprite(Sprite newSprite )
    {
        m_productImage.sprite = newSprite;
    }

    public void SetItemIndex(int newIndex )
    {
        m_productIndex = newIndex;
    }

    public void SetItemPrice(int newPrice )
    {
        m_productPrice = newPrice;
    }

    public void SetItemDescription( TextAsset newDescription )
    {
        m_productDescriptionFile = newDescription;
    }

    public void OnClick( )
    {
        //Sets the text of the itemDescription UI element to that contained within the button's product description file
        m_itemDescriptionUI.text = m_productDescriptionFile.text;

        //Sets the item index of the store controller to that of the button, so the correct product can be bought
        m_storeController.SetItemIndex( m_productIndex );

        //Sets the price of the selected item in the store controller to that of the button, so the player will be charged correctly
        m_storeController.SetItemPrice( m_productPrice );

        //Sets the text of the buy button's price text element to the value stored in the button so the correct price is displayed to the player
        m_itemPriceText.text = m_productPrice.ToString( );
    }

}
