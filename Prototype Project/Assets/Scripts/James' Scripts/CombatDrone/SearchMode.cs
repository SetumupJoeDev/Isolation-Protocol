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
                    if ( FindLootNode( ) )
                    {
                        m_droneController.DisableBasicBehaviours( );
                        m_currentSearchState = searchState.movingToNode;
                    }
            }
            break;

            case ( searchState.movingToNode ):
            {
                    MoveToNode( );
            }
            break;

            case ( searchState.searchingForLoot ):
            {
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

        Collider2D lootNode = Physics2D.OverlapCircle( transform.position , m_searchRadius , m_searchNodeLayer );

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

        gameObject.transform.position = Vector3.MoveTowards( transform.position , m_currentLootNode.transform.position , m_moveToNodeSpeed * Time.deltaTime );

        if( Vector3.Distance(transform.position, m_currentLootNode.transform.position) < Mathf.Epsilon )
        {
            m_currentSearchState = searchState.searchingForLoot;
        }

    }

    public IEnumerator SearchForLoot( )
    {

        m_searchingForLoot = true;

        yield return new WaitForSeconds( m_searchTime );

        m_currentLootNode.GetComponent<SearchNode>( ).GenerateLoot( );

        m_currentSearchState = searchState.findingNode;

        m_searchingForLoot = false;

        m_droneController.EnableBasicBehaviours( );

    }

}
