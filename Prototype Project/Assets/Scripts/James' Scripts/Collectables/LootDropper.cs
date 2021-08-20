using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{

    #region Loot

    [Header("Loot")]

    [Range(1, 3)]
    [Tooltip("The modifier bonus to any loot drops this enemy has. Has a base of one, but can be increased.")]
    public float m_lootBoostModifier = 1.0f;

    [Tooltip("The maximum number of cigarette packs this enemy will drop when killed.")]
    public int m_maxCigaretteDrops;

    [Tooltip("The minimum number of cigarette packs this enemy will drop when killed.")]
    public int m_minCigaretteDrops;

    [Tooltip("The prefab for the cigarette pack gameobject.")]
    public GameObject m_cigPackPrefab;

    [Tooltip("The maximum number of fabricator fuel tanks this enemy will drop when killed.")]
    public int m_maxFuelDrops;

    [Tooltip("The minimum number of fabricator fuel tanks this enemy will drop when killed.")]
    public int m_minFuelDrops;

    [Tooltip("The prefab for the cigarette pack gameobject.")]
    public GameObject m_fabricatorFuelPrefab;

    [Tooltip("A boolean that determines whether or not this object should drop a weapon.")]
    public bool m_canDropWeapons;

    [Tooltip("The array of weapon prefabs that this script can generate in a loot drop.")]
    public GameObject[] m_weaponDrops;

    [Tooltip("The amount of force with which the generated weapon is pushed from the loot node.")]
    public float m_weaponForceSpeed;

    #endregion

    public virtual void DropLoot()
    {

        DropCigarettes( );

        DropFuel( );

        if ( m_canDropWeapons )
        {
            DropWeapon( );
        }
        
    }

    public void DropCigarettes()
    {
        //If the enemy is set to drop more than 0 cigarettes as their maximum, a random number of them is dropped
        if (m_maxCigaretteDrops > 0)
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float cigsToDrop = Random.Range(m_minCigaretteDrops * m_lootBoostModifier, m_maxCigaretteDrops * m_lootBoostModifier);

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            cigsToDrop = Mathf.Round(cigsToDrop);

            //Loops for the number generated above to generate currency drops
            for (int i = 0; i < cigsToDrop; i++)
            {
                //Instantiates a new cigarette packet and saves the base class as a local variable for later use
                CurrencyBase newCigPack = Instantiate(m_cigPackPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                AddForceToDrop( newCigPack.m_rigidBody, newCigPack.m_gravitationalSpeed );

            }
        }
    }

    public void DropFuel()
    {
        //If the enemy is set to drop more than 0 fuel as their maximum, a random number of them is dropped
        if (m_maxFuelDrops > 0)
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float fuelToDrop = Random.Range(m_minFuelDrops * m_lootBoostModifier, m_maxFuelDrops * m_lootBoostModifier);

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            fuelToDrop = Mathf.Round(fuelToDrop);

            //Loops for the number generated above to generate currency drops
            for (int i = 0; i < fuelToDrop; i++)
            {

                //Instantiates a new fabricator fuel and saves the base class as a local variable for later use
                CurrencyBase newFuelTank = Instantiate(m_fabricatorFuelPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                AddForceToDrop( newFuelTank.m_rigidBody, newFuelTank.m_gravitationalSpeed );
                
            }
        }
    }

    public void DropWeapon()
    {

        //Generates a random index within the range of the array
        int randIndex = Random.Range( 0, m_weaponDrops.Length - 1 );

        //Instantiates a new weapon based on the prefab at the address in the array generated above
        DroppableWeapon newWeapon = Instantiate( m_weaponDrops[randIndex], transform.position, Quaternion.identity ).GetComponent<DroppableWeapon>( );

        AddForceToDrop( newWeapon.GetComponent<Rigidbody2D>(), m_weaponForceSpeed );

    }

    private void AddForceToDrop( Rigidbody2D rigidBody, float gravitationSpeed )
    {
        //Generates a random number between -100 and 100 to be used in adding force to the currency so that they are ejected from the enemy and scattered
        float randX = Random.Range(-100, 100);
        float randY = Random.Range(-100, 100);

        //Adds force using the numbers generated above to scatter the cigarettes
        rigidBody.AddForce(new Vector2(randX, randY).normalized * gravitationSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

}
