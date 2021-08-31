using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewProduct" , menuName = "ScriptableObjects/FabricatorProduct" , order = 1 )]
public class FabricatorStoreProduct : ScriptableObject
{

    [Tooltip("The price of the item.")]
    public int m_itemPrice;

    [Tooltip("The item's sprite.")]
    public Sprite m_itemSprite;

    [Tooltip("The name of the item.")]
    public string m_itemName;

    [Tooltip("The product description of the item.")]
    public TextAsset m_itemDescription;

    [Tooltip("A boolean that determines whether or not this product is unlocked.")]
    public bool m_isUnlocked;

    public void Start( )
    {
        m_isUnlocked = false;
    }

    public string GetItemName( )
    {
        return m_itemName;
    }

    public Sprite GetItemSprite( )
    {
        return m_itemSprite;
    }

    public int GetItemPrice( )
    {
        return m_itemPrice;
    }

    public TextAsset GetItemDescription( )
    {
        return m_itemDescription;
    }

    public void SetIsUnlocked( bool isUnlocked )
    {
        m_isUnlocked = isUnlocked;
    }

    public bool GetIsUnlocked( )
    {
        return m_isUnlocked;
    }

}
