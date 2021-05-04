using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{

    [Header("Loot Table & Probabilities")]

    [SerializeField]
    private int m_tierDProbability;
    
    [SerializeField]
    private int m_tierCProbability;
    
    [SerializeField]
    private int m_tierBProbability;

    [SerializeField]
    private int m_tierAProbability;

    [SerializeField]
    private int m_tierSProbability;

    private int[] m_lootTable;

    [SerializeField]
    private int m_totalProbability;

    private enum weaponTier { dTier, cTier, bTier, aTier, sTier };

    [Header("Weapons")]

    private weaponTier m_generatedWeaponTier;

    public GameObject[] m_tierDWeapons;

    public GameObject[] m_tierCWeapons;
    
    public GameObject[] m_tierBWeapons;
    
    public GameObject[] m_tierAWeapons;
    
    public GameObject[] m_tierSWeapons;

    [SerializeField]
    private GameObject m_purchasableWeaponPrefab;

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

        CalculateTotalProbability( );

        GameObject newHealthItem  = Instantiate( m_healthItemPrefab , m_healthItemTransform.position , Quaternion.identity );

        newHealthItem.transform.parent = m_healthItemTransform;

        GameObject newAmmoItem = Instantiate( m_ammoItemPrefab , m_ammoItemTransform.position , Quaternion.identity );

        newAmmoItem.transform.parent = m_ammoItemTransform;

        GenerateRandomWeapon( );

    }

    private void GenerateRandomWeapon( )
    {
        SelectRandomWeaponTier( );
    }

    private void CalculateTotalProbability( )
    {
        foreach( int value in m_lootTable )
        {
            m_totalProbability += value;
        }
    }

    private void SelectRandomWeaponTier( )
    {

        int randInt = Random.Range(1, m_totalProbability);

        Debug.Log( randInt );

        for( int i = 0; i < m_lootTable.Length; i++ )
        {
            Debug.Log( randInt );

            if( randInt <= m_lootTable[i] )
            {
                m_generatedWeaponTier = ( weaponTier )i;
                SelectWeaponFromTier( );
                break;
            }
            else
            {
                randInt -= m_lootTable[i];
            }
        }
    }

    private void SelectWeaponFromTier( )
    {

        int randInt = 0;

        GameObject newWeapon = null;

        switch ( m_generatedWeaponTier )
        {
            case weaponTier.dTier:
                {
                    randInt = Random.Range( 0 , m_tierDWeapons.Length - 1 );
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierDWeapons[randInt];
                }
                break;
            case weaponTier.cTier:
                {
                    randInt = Random.Range( 0 , m_tierCWeapons.Length - 1 );
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierCWeapons[randInt];
                }
                break;
            case weaponTier.bTier:
                {
                    randInt = Random.Range( 0 , m_tierBWeapons.Length - 1 );
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierBWeapons[randInt];
                }
                break;
            case weaponTier.aTier:
                {
                    randInt = Random.Range( 0 , m_tierAWeapons.Length - 1 );
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierAWeapons[randInt];
                }
                break;
            case weaponTier.sTier:
                {
                    randInt = Random.Range( 0 , m_tierSWeapons.Length - 1 );
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierSWeapons[randInt];
                }
                break;
        }

        newWeapon.transform.parent = m_weaponTransform;
        Debug.Log( "Generated: " + newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab.name );
    }

}
