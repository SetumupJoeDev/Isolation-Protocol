using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InterfaceButton : MonoBehaviour
{

    [Tooltip("The image that represents the product.")]
    public Image m_productImage;

    [Tooltip("The name of the product.")]
    public Text m_productName;

    [Tooltip("The button's numerical ID.")]
    public int m_buttonID;

    public virtual void OnClick( )
    {
        //On click behaviour goes here

    }

}
