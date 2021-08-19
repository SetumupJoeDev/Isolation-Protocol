using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefibMode : PassiveDroneBehaviourBase
{

    [Tooltip("A boolean that determines whether or not the drone can defib the player. Set to false after one use.")]
    public bool m_canDefibPlayer;

    [Tooltip("The amount of time it takes for the drone to defib the player.")]
    public int m_defibTime;

    [Tooltip("The HealthManager script attached to the player.")]
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
        //Waits for the defib timer to run out before reviving the player at full health and setting canDefibPlayer to false, as this can only be used once per run
        yield return new WaitForSeconds( m_defibTime );

        m_playerHealthManager.m_currentPlayerState = PlayerHealthManager.playerState.alive;

        m_playerHealthManager.m_currentHealth = m_playerHealthManager.m_maxHealth;

        m_canDefibPlayer = false;

    }

}
