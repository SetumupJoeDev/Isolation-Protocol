using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{

    public int[] m_lootTable = { 40, 20, 20, 10, 5, 5 };

    public GameObject m_healthItemPrefab;

    public GameObject m_ammoItemPrefab;

    public Transform m_healthItemTransform;

    public Transform m_ammoItemTransform;

    // Start is called before the first frame update
    void Start()
    {

        Instantiate( m_healthItemPrefab , m_healthItemTransform.position , Quaternion.identity );

        Instantiate( m_ammoItemPrefab , m_ammoItemTransform.position , Quaternion.identity );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
