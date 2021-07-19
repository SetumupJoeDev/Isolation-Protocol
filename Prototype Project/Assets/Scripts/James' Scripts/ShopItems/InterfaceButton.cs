using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InterfaceButton : MonoBehaviour
{

    [SerializeField]
    public Image m_productImage;

    [SerializeField]
    public Text m_productName;

    public int m_buttonID;

    public virtual void OnClick( )
    {

    }

}
