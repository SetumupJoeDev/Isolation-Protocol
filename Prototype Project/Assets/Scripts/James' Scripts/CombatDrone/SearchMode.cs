using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMode : DroneBehaviourBase
{

    [Tooltip("The amount of time it takes the drone to uncover loot at a search point.")]
    public float m_searchTime;

    [Tooltip("The radius in which the drone will check for loot nodes.")]
    public float m_searchRadius;

    [Tooltip("A boolean that determines whether or not the drone is currently searching a loot node.")]
    public bool m_searchingForLoot;

    [Tooltip("The speed at which the drone moves towards a loot note when it has found one.")]
    public float m_moveToNodeSpeed;

    [Tooltip("The loot note that the drone is currently searching.")]
    public GameObject m_currentLootNode;

    [Tooltip("The physics layer that the loot nodes sit on.")]
    public LayerMask m_searchNodeLayer;

    public enum searchState { findingNode, movingToNode, searchingForLoot}

    [Tooltip("The current state the drone is in.")]
    public searchState m_currentSearchState;

    private void Update( )
    {

        switch ( m_currentSearchState )
        {
            case ( searchState.findingNode ):
            {
                    //If the drone finds a loot node, it moves towards it and disables the drone's basic movement and combat behaviours temportarily
                    if ( FindLootNode( ) )
                    {
                        m_droneController.DisableBasicBehaviours( );
                        m_currentSearchState = searchState.movingToNode;
                    }
            }
            break;

            case ( searchState.movingToNode ):
            {
                //Moves the drone toward the loot node it has found
                MoveToNode( );
            }
            break;

            case ( searchState.searchingForLoot ):
            {
                    //If the drone isn't currently searching for loot, the SearchForLoot coroutine is started to generate loot
                    if ( !m_searchingForLoot )
                    {
                        StartCoroutine( SearchForLoot( ) );
                    }
            }
            break;
        }


    }

    public bool FindLootNode( )
    {

        //Uses an overlap circle to search for objects on the loot node layer
        Collider2D lootNode = Physics2D.OverlapCircle( transform.position , m_searchRadius , m_searchNodeLayer );

        //If a loot node is found, it is saved as the drone's current loot node to search
        if( lootNode != null )
        {
            m_currentLootNode = lootNode.gameObject;
            return true;
        }

        else
        {
            return false;
        }

    }

    public void MoveToNode( )
    {

        //Moves the drone towards the loot node using deltaTime to make it framerate independant
        gameObject.transform.position = Vector3.MoveTowards( transform.position , m_currentLootNode.transform.position , m_moveToNodeSpeed * Time.deltaTime );

        //If the drone is within very close proximity to the loot node, we assume it has reached it and so it begins searching for loot
        if( Vector3.Distance(transform.position, m_currentLootNode.transform.position) < Mathf.Epsilon )
        {
            m_currentSearchState = searchState.searchingForLoot;
        }

    }

    public IEnumerator SearchForLoot( )
    {

        m_searchingForLoot = true;

        //Waits for the duration of the drone's search time before generating loot
        yield return new WaitForSeconds( m_searchTime );

        //Calls the GenerateLoot function of the loot node to produce currency/weapons
        m_currentLootNode.GetComponent<SearchNode>( ).GenerateLoot( );

        //Returns the drone to its node-finding state
        m_currentSearchState = searchState.findingNode;

        m_searchingForLoot = false;

        //Re-enables the drone's basic functionalities
        m_droneController.EnableBasicBehaviours( );

    }

}
