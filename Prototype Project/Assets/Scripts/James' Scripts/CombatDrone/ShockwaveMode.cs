using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMode : ActiveDroneBehaviourBase
{

    #region Shockwave Settings

    [Header("Shockwave Settings")]

    [Tooltip("The width of the line rendered to represent the shockwave.")]
    public float m_shockwaveLineWidth;

    [Tooltip("The material applied to the shockwave line renderer.")]
    public Material m_shockwaveMaterial;

    [Tooltip("The amount of knockback applied to characters hit by the shockwave.")]
    public float m_knockbackForce;

    [Tooltip("The amount of damage dealt to characters hit by the shockwave.")]
    public int m_shockwaveDamage;

    [Tooltip("The duration for which the characters hit by the shockwave experience knockback")]
    public float m_knockbackDuration;

    [Tooltip("The maximum size that the shockwave will grow to before stopping.")]
    public float m_shockwaveMaxRadius;

    [Tooltip("The speed at which the shockwave's radius grows.")]
    public float m_shockwaveSpeed;

    [Tooltip("The number of vertices in the shockwave's line renderer. The higher the number the smoother the circle will be.")]
    public int m_shockwaveVertices;

    [Tooltip("The layer upon which targets of the shockwave sit.")]
    public LayerMask m_targetLayer;

    #endregion

    //The shockwave script attached to this object
    private Shockwave m_shockwave;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake( );

        //Creates a shockwave component and adds it to the GameObject, and also assigns it
        m_shockwave = gameObject.AddComponent( typeof( Shockwave ) ) as Shockwave;

        //Assigns all of the values required by the shockwave
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

        //Assigns all of the necessary values to the shockwave component
        m_shockwave.m_shockwaveDamage = m_shockwaveDamage;

        m_shockwave.m_shockwaveLineWidth = m_shockwaveLineWidth;

        m_shockwave.m_knockbackForce = m_knockbackForce;

        m_shockwave.m_knockbackDuration = m_knockbackDuration;

        m_shockwave.m_shockwaveMaterial = m_shockwaveMaterial;

        m_shockwave.m_shockwaveMaxRadius = m_shockwaveMaxRadius;

        m_shockwave.m_shockwaveSpeed = m_shockwaveSpeed;

        m_shockwave.m_shockwaveVertCount = m_shockwaveVertices;

        m_shockwave.m_targetLayer = m_targetLayer;

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
