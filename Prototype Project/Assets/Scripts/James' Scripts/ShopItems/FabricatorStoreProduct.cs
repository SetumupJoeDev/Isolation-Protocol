using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewProduct" , menuName = "ScriptableObjects/FabricatorProduct" , order = 1 )]
public class FabricatorStoreProduct : ScriptableObject
{

    [SerializeField]
    private int m_itemPrice;

    [SerializeField]
    private Sprite m_itemSprite;

    [SerializeField]
    private string m_itemName;

    [SerializeField]
    private TextAsset m_itemDescription;

    [SerializeField]
    private bool m_isUnlocked;

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
