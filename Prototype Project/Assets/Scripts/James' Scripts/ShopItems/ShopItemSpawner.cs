using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{

    [Header("Loot Table & Probabilities")]

    public int m_tierDProbability;

    public int m_tierCProbability;

    public int m_tierBProbability;

    public int m_tierAProbability;

    public int m_tierSProbability;

    public int m_numTiers;

    private int[] m_lootTable;

    private int m_weaponTierIndex;

    private int m_weaponTypeIndex;

    [Header("Weapons")]


    public GameObject[] m_tierDWeapons;

    public GameObject[] m_tierCWeapons;
    
    public GameObject[] m_tierBWeapons;
    
    public GameObject[] m_tierAWeapons;
    
    public GameObject[] m_tierSWeapons;

    public GameObject[][] m_weaponPrefabs;

    public Transform m_weaponTransform;

    [Space]

    [Header("Health & Ammo")]

    [Tooltip("The prefabbed GameObject used for the purchasable health item.")]
    public GameObject m_healthItemPrefab;

    [Tooltip("The prefabbed GameObject used for the purchasable ammo item.")]
    public GameObject m_ammoItemPrefab;

    [Tooltip("The transform used for the placement of the purchasable health item.")]
    public Transform m_healthItemTransform;

    [Tooltip("The transform used for the placement of the purchasable ammo item.")]
    public Transform m_ammoItemTransform;

    // Start is called before the first frame update
    void Start()
    {

        m_lootTable = new int[] { m_tierDProbability, m_tierCProbability, m_tierBProbability, m_tierAProbability, m_tierSProbability };

        PopulateWeaponArray( );

        Instantiate( m_healthItemPrefab , m_healthItemTransform.position , Quaternion.identity );

        Instantiate( m_ammoItemPrefab , m_ammoItemTransform.position , Quaternion.identity );

        SelectWeaponTier( );

    }

    private void PopulateWeaponArray( )
    {
        for( int i = 0; i < m_numTiers; i++ )
        {
            for( int j = 0; j < m_tierDWeapons.Length; j++ )
            {
                m_weaponPrefabs[i][j] = m_tierDWeapons[j];
            }
        }
    }

    private void SelectWeaponTier( )
    {

        int randInt = Random.Range(0, m_tierDProbability);

        for( int i = 0; i < m_lootTable.Length; i++ )
        {
            if( randInt < m_lootTable[i] )
            {
                m_weaponTierIndex = i;
                SelectWeaponFromTier( );
            }
        }
    }

    private void SelectWeaponFromTier( )
    {

        m_weaponTypeIndex = Random.Range( 0 , m_weaponPrefabs.Length );

        Instantiate( m_weaponPrefabs[m_weaponTierIndex][m_weaponTypeIndex] , m_weaponTransform.position , Quaternion.identity );

    }

}
