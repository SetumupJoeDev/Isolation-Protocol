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
        //Adds the probabilities of getting each tier of weapon to the loot table
        m_lootTable = new int[] { m_tierDProbability, m_tierCProbability, m_tierBProbability, m_tierAProbability, m_tierSProbability };

        //Calculates the total probability by combining all the values in the array
        CalculateTotalProbability( );

        //Creates a new healing item at the appropriate transform position
        GameObject newHealthItem  = Instantiate( m_healthItemPrefab , m_healthItemTransform.position , Quaternion.identity );

        //Sets the transform parent of the healing item to be the health item's positioning transform to keep the heirarchy tidy
        newHealthItem.transform.parent = m_healthItemTransform;

        //Creates a new ammo item at the appropriate transform position
        GameObject newAmmoItem = Instantiate( m_ammoItemPrefab , m_ammoItemTransform.position , Quaternion.identity );

        //Sets the transform parent of the ammo item to be the ammo item's positioning transform to keep the heirarchy tidy
        newAmmoItem.transform.parent = m_ammoItemTransform;

        //Generates a random weapon from the list using the values in the loot table
        GenerateRandomWeapon( );

    }

    private void GenerateRandomWeapon( )
    {
        SelectRandomWeaponTier( );
    }

    private void CalculateTotalProbability( )
    {
        //Loops through each value in the loot table, adding them up to calculate a total probability
        foreach( int value in m_lootTable )
        {
            m_totalProbability += value;
        }
    }

    private void SelectRandomWeaponTier( )
    {
        //Generates a random number between 1 and the total probability
        int randInt = Random.Range(1, m_totalProbability);

        //Loops through each value in the table, checking to see if the number generated above is less than or equal to each probability
        for( int i = 0; i < m_lootTable.Length; i++ )
        {
            //If the number generated above is less than or equal to the value at the current index, then a weapon selected from the corresponding tier
            if( randInt <= m_lootTable[i] )
            {
                m_generatedWeaponTier = ( weaponTier )i;
                SelectWeaponFromTier( );
                break;
            }
            else
            {
                //Otherwise, the value at the current index is subtracted from the generated value and checked again
                randInt -= m_lootTable[i];
            }
        }
    }

    private void SelectWeaponFromTier( )
    {

        int randInt = 0;

        GameObject newWeapon = null;

        //Switches through each weapon tier and acts accordingly
        switch ( m_generatedWeaponTier )
        {
            case weaponTier.dTier:
                {
                    //Generates a random number between 0 and the length of the weapon tier array -1 to stay within the bounds
                    randInt = Random.Range( 0 , m_tierDWeapons.Length - 1 );
                    //Instantiates a new weapon product at the appropriate position and assigns it the weapon prefab from the corresponding array index
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierDWeapons[randInt];
                }
                break;
            case weaponTier.cTier:
                {
                    //Generates a random number between 0 and the length of the weapon tier array -1 to stay within the bounds
                    randInt = Random.Range( 0 , m_tierCWeapons.Length - 1 );
                    //Instantiates a new weapon product at the appropriate position and assigns it the weapon prefab from the corresponding array index
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierCWeapons[randInt];
                }
                break;
            case weaponTier.bTier:
                {
                    //Generates a random number between 0 and the length of the weapon tier array -1 to stay within the bounds
                    randInt = Random.Range( 0 , m_tierBWeapons.Length - 1 );
                    //Instantiates a new weapon product at the appropriate position and assigns it the weapon prefab from the corresponding array index
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierBWeapons[randInt];
                }
                break;
            case weaponTier.aTier:
                {
                    //Generates a random number between 0 and the length of the weapon tier array -1 to stay within the bounds
                    randInt = Random.Range( 0 , m_tierAWeapons.Length - 1 );
                    //Instantiates a new weapon product at the appropriate position and assigns it the weapon prefab from the corresponding array index
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierAWeapons[randInt];
                }
                break;
            case weaponTier.sTier:
                {
                    //Generates a random number between 0 and the length of the weapon tier array -1 to stay within the bounds
                    randInt = Random.Range( 0 , m_tierSWeapons.Length - 1 );
                    //Instantiates a new weapon product at the appropriate position and assigns it the weapon prefab from the corresponding array index
                    newWeapon = Instantiate( m_purchasableWeaponPrefab , m_weaponTransform.position , Quaternion.identity );
                    newWeapon.GetComponent<WeaponShopItem>( ).m_weaponPrefab = m_tierSWeapons[randInt];
                }
                break;
        }
        //Parents the newly instantiated weapon to the weapon transform position to keep the heirarchy tidy
        newWeapon.transform.parent = m_weaponTransform;
    }

}
