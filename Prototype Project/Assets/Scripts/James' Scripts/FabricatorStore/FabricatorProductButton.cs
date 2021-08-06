using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FabricatorProductButton : InterfaceButton
{

    [SerializeField]
    private FabricatorStoreBase m_storeController;

    public FabricatorUpgradeListGenerator m_buttonList;

    public FabricatorStoreProduct m_storeProduct;

    public bool m_isSelected;

    public AudioClip m_insufficientFundsClip;

    [SerializeField]
    private int m_productPrice;

    private int m_productIndex;

    [SerializeField]
    private TextAsset m_productDescriptionFile;

    [SerializeField]
    private TextMeshProUGUI m_itemDescriptionUI;

    [SerializeField]
    private TextMeshProUGUI m_itemPriceText;

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
        if ( !m_storeProduct.GetIsUnlocked( ) )
        {
            m_itemPriceText.text = m_productPrice.ToString();
        }
        else
        {
            m_itemPriceText.text = "Out Of Stock";
        }
    }

    public void SetItemDescription( TextAsset newDescription )
    {
        m_productDescriptionFile = newDescription;
    }

    public override void OnClick( )
    {

        if ( m_buttonList.m_activeButton != this )
        {

            if ( m_buttonList.m_activeButton != null )
            {
                m_buttonList.m_activeButton.m_isSelected = false;
            }

            m_buttonList.m_activeButton = this;

            UpdateProductInfo( );

        }
        else if ( m_storeController.PlayerCanAfford( m_productPrice ) )
        {
            
            m_storeProduct.SetIsUnlocked( true );

            UpdateProductInfo( );

        }
        else
        {
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
            m_itemPriceText.text = "Out Of Stock";
        }
    }

}
