using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNode : MonoBehaviour
{

    public int m_maxCiggiesToGen;

    public int m_minCiggiesToGen;

    public int m_maxFuelToGen;

    public int m_minFuelToGen;

    public GameObject m_cigPackPrefab;

    public GameObject m_fabricatorFuelPrefab;

    public void GenerateLoot( )
    {
        //If the enemy is set to drop more than 0 cigarettes as their maximum, a random number of them is dropped
        if ( m_maxCiggiesToGen > 0 )
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float cigsToDrop = Random.Range(m_minCiggiesToGen, m_maxCiggiesToGen);

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            cigsToDrop = Mathf.Round( cigsToDrop );

            //Loops for the number generated above to generate currency drops
            for ( int i = 0; i < cigsToDrop; i++ )
            {
                //Instantiates a new cigarette packet and saves the base class as a local variable for later use
                CurrencyBase newCigPack = Instantiate( m_cigPackPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                //Generates a random number between -100 and 100 to be used in adding force to the currency so that they are ejected from the enemy and scattered
                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                //Adds force using the numbers generated above to scatter the cigarettes
                newCigPack.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newCigPack.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }
        //If the enemy is set to drop more than 0 fuel as their maximum, a random number of them is dropped
        if ( m_maxFuelToGen > 0 )
        {
            //Generates a random number between the minimum and maximum drops multiplied by the loost boost modifier
            float fuelToDrop = Random.Range( m_minFuelToGen, m_maxFuelToGen );

            //Rounds the number to an integer so it can be used in the for loop to generate drops
            fuelToDrop = Mathf.Round( fuelToDrop );

            //Loops for the number generated above to generate currency drops
            for ( int i = 0; i < fuelToDrop; i++ )
            {

                //Instantiates a new fabricator fuel and saves the base class as a local variable for later use
                CurrencyBase newFuelTank = Instantiate(m_fabricatorFuelPrefab, transform.position, Quaternion.identity).GetComponent<CurrencyBase>();

                //Generates a random number between -100 and 100 to be used in adding force to the currency so that they are ejected from the enemy and scattered
                float randX = Random.Range(-100, 100);
                float randY = Random.Range(-100, 100);

                //Adds force using the numbers generated above to scatter the cigarettes
                newFuelTank.m_rigidBody.AddForce( new Vector2( randX , randY ).normalized * newFuelTank.m_gravitationalSpeed * Time.fixedDeltaTime , ForceMode2D.Impulse );

            }
        }

        Destroy( gameObject );

    }
}
