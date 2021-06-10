using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefibMode : PassiveDroneBehaviourBase
{

    public bool m_canDefibPlayer;

    [SerializeField]
    private int m_defibTime;

    public PlayerHealthManager m_playerHealthManager;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake( );
        //Sets this to true so that the drone starts off with the ability to revive the player
        m_canDefibPlayer = true;
    }

    private void Start( )
    {
        m_canDefibPlayer = true;

        m_droneController = GetComponent<DroneController>( );

    }

    public void AttemptDefibrillation( )
    {
        //If the drone has not yet attempted to revive the player on this run, this will be true and the coroutine will be executed
        if ( m_canDefibPlayer )
        {
            StartCoroutine( DefibTimer( ) );
        }
    }

    private IEnumerator DefibTimer( )
    {
        //Waits for the defib timer to run out before reviving the player and setting canDefibPlayer to false, as this can only be used once per run
        yield return new WaitForSeconds( m_defibTime );

        m_playerHealthManager.m_currentPlayerState = PlayerHealthManager.playerState.alive;

        m_playerHealthManager.m_currentHealth = m_playerHealthManager.m_maxHealth;

        m_canDefibPlayer = false;

    }

}
