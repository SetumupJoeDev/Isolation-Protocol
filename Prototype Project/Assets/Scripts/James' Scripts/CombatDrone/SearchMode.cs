using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMode : DroneBehaviourBase
{

    public float m_searchTime;

    public float m_searchRadius;

    public bool m_searchingForLoot;

    public float m_moveToNodeSpeed;

    public GameObject m_currentLootNode;

    public LayerMask m_searchNodeLayer;

    public enum searchState { findingNode, movingToNode, searchingForLoot}

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
