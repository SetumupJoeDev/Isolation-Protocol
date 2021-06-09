using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMode : ActiveDroneBehaviourBase
{

    #region Shockwave Settings

    [Header("Shockwave Settings")]

    [SerializeField]
    private float m_shockwaveLineWidth;

    [SerializeField]
    private Material m_shockwaveMaterial;

    [SerializeField]
    private float m_knockbackForce;

    [SerializeField]
    private int m_shockwaveDamage;

    [SerializeField]
    private float m_knockbackDuration;

    [SerializeField]
    private float m_shockwaveMaxRadius;

    [SerializeField]
    private float m_shockwaveSpeed;

    [SerializeField]
    private int m_shockwaveVertices;

    [SerializeField]
    private LayerMask m_targetLayer;

    #endregion

    private Shockwave m_shockwave;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake( );

        m_shockwave = gameObject.AddComponent( typeof( Shockwave ) ) as Shockwave;

        InitialiseShockwaveValues( );

        m_shockwave.enabled = false;

    }

    private void OnDisable( )
    {
        //Destroys the shockwave renderer component as it is no longer needed while this upgrade is inactive
        m_shockwave.enabled = false;
    }

    public override void Update( )
    {

        base.Update( );

        if ( m_shockwave.m_shockwaveEnded )
        {
            DisableModuleBehaviour( );

            m_shockwave.m_shockwaveEnded = false;

        }

        //If the behaviour is active, the shockwave is grown from the center, checking for enemies, and drawn
        if ( m_behaviourActive && !m_shockwave.isActiveAndEnabled )
        {
            m_shockwave.enabled = true;
        }

    }

    public void InitialiseShockwaveValues( )
    {
        m_shockwave.m_shockwaveDamage = m_shockwaveDamage;

        m_shockwave.m_shockwaveLineWidth = m_shockwaveLineWidth;

        m_shockwave.m_knockbackForce = m_knockbackForce;

        m_shockwave.m_knockbackDuration = m_knockbackDuration;

        m_shockwave.m_shockwaveMaterial = m_shockwaveMaterial;

        m_shockwave.m_shockwaveMaxRadius = m_shockwaveMaxRadius;

        m_shockwave.m_shockwaveSpeed = m_shockwaveSpeed;

        m_shockwave.m_shockwaveVertCount = m_shockwaveVertices;

    }

    public override void EnableModuleBehaviour( )
    {
        //Disables the drone's basic behaviours so it can't follow the player or shoot enemies
        m_droneController.DisableBasicBehaviours( );

        //Enables the shockwave line renderer
        m_shockwave.enabled = true;

        //Enables the behaviour so that the necessary logic is executed
        m_behaviourActive = true;
    }

    public override void DisableModuleBehaviour( )
    {
        m_behaviourActive = false;

        m_droneController.EnableBasicBehaviours( );

        StartCoroutine( CooldownTimer( ) );

    }

}
