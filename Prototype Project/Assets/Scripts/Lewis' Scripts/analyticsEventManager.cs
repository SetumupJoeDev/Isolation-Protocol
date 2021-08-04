using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analyticsEventManager : MonoBehaviour
{
    public static analyticsEventManager current;

    public void Awake()
    {
        current = this;
    }


    public event Action boltShootIncrement;





  




public void onBoltShoot()
    {
        if (boltShootIncrement != null)
        {
            boltShootIncrement();
        }
    }
}
