﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FabricatorProductButton : MonoBehaviour
{

    [SerializeField]
    private Image m_productImage;

    [SerializeField]
    private Text m_productName;

    [SerializeField]
    private int m_productPrice;

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
        m_itemDescriptionUI.text = m_productDescriptionFile.text;

        m_itemPriceText.text = m_productPrice.ToString( );
    }

}