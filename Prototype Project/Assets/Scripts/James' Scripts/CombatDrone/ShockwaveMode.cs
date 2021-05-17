using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMode : ActiveDroneBehaviourBase
{

    #region Shockwave Drawing

    [Header("Shockwave Drawing")]

    [SerializeField]
    private LineRenderer m_shockwaveRenderer;

    [SerializeField]
    private int m_shockwaveVertCount;

    [SerializeField]
    private float m_shockwaveLineWidth;

    [SerializeField]
    private Material m_shockwaveMaterial;

    [Space]

    #endregion

    #region Shockwave Logic

    [Header("Shockwave Attack")]

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
    private float m_currentShockwaveRadius;

    [SerializeField]
    private LayerMask m_targetLayer;

    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake( );

        //Sets up the shockwave's line renderer so it can be used to visualise the effect
        SetUpRenderer( );

    }

    private void OnDisable( )
    {
        //Destroys the shockwave renderer component as it is no longer needed while this upgrade is inactive
        Destroy( m_shockwaveRenderer );
    }

    public override void Update( )
    {

        base.Update( );

        //If the behaviour is active, the shockwave is grown from the center, checking for enemies, and drawn
        if( m_behaviourActive )
        {
            GrowShockwave( );
            DrawShockwave( );
        }
    }

    private void SetUpRenderer( )
    {
        //Adds a LineRenderer component to the drone, saving it to shockwaveRenderer so that the shockwave circle can be drawn
        m_shockwaveRenderer = gameObject.AddComponent( typeof( LineRenderer ) ) as LineRenderer;

        //Sets this to true so that the first and last points in the line can be connected to form a circle
        m_shockwaveRenderer.loop = true;

        //Sets the material so that the line can be rendered with a graphic
        m_shockwaveRenderer.material = m_shockwaveMaterial;

        //Sets the sorting order so that the shockwave will be rendered over the floor and walls
        m_shockwaveRenderer.sortingOrder = 5;

        //Sets the line width of the line renderer so that the shockwave will have a set thickness
        m_shockwaveRenderer.widthMultiplier = m_shockwaveLineWidth;
    }

    public void GrowShockwave( )
    {
        //Uses an overlap circle to find all enemies within the current radius and adds them to an array
        Collider2D[] affectedEnemies = Physics2D.OverlapCircleAll(transform.position, m_currentShockwaveRadius, m_targetLayer );

        //Checks to see if there are any enemies in the array
        if ( affectedEnemies.Length >= 1 )
        {
            //Loops through the array of enemies, starting their knockback coroutines, and damaging them
            for ( int i = 0; i < affectedEnemies.Length; i++ )
            {
                StartCoroutine( affectedEnemies[i].GetComponent<CharacterBase>( ).KnockBack( m_knockbackForce, m_knockbackDuration , gameObject ) );

                affectedEnemies[i].gameObject.GetComponent<HealthManager>( ).TakeDamage( m_shockwaveDamage );

            }
        }

        //Increases the radius of the shockwave by shockwaveSpeed multiplied by deltaTime, so that the shockwave grows over time
        m_currentShockwaveRadius += m_shockwaveSpeed * Time.deltaTime;

        //If the radius is greater than or equal to the maximum radius, then the shockwave stops
        if ( m_currentShockwaveRadius >= m_shockwaveMaxRadius )
        {
            //The drone's default behaviours are reactivated so it can follow the player and shoot at enemies
            m_droneController.EnableBasicBehaviours( );

            //Resets the current radius of the shockwave so it can be used again
            m_currentShockwaveRadius = 0.0f;

            //Disables the shockwave renderer to stop rendering the shockwave circle
            m_shockwaveRenderer.enabled = false;

            //Disables the behaviour so it can be used again after cooldown
            m_behaviourActive = false;

            //Starts the cooldown coroutine
            StartCoroutine( CooldownTimer( ) );

        }
    }

    public void DrawShockwave( )
    {

        //Calculates the distance between each vertex in the circle
        float deltaTheta = (2f * Mathf.PI) / m_shockwaveVertCount;
        float theta = 0.0f;

        //Sets the number of positions in the line to equal the vertex count
        m_shockwaveRenderer.positionCount = m_shockwaveVertCount;

        //Loops for the number of vertices in the line, positioning each around in a circle
        for( int i = 0; i < m_shockwaveRenderer.positionCount; i++ )
        {
            //Positions the current vertex using the values previously calculated, adding the transform positions so that they follow the drone
            Vector3 pos = new Vector3( m_currentShockwaveRadius * Mathf.Cos( theta ) + transform.position.x , m_currentShockwaveRadius * Mathf.Sin( theta ) + transform.position.y , 0.0f );
            m_shockwaveRenderer.SetPosition( i , pos );

            //Increases the value of theta by the distance between points
            theta += deltaTheta;
        }

    }

    public override void EnableModuleBehaviour( )
    {
        //Disables the drone's basic behaviours so it can't follow the player or shoot enemies
        m_droneController.DisableBasicBehaviours( );

        //Enables the shockwave line renderer
        m_shockwaveRenderer.enabled = true;

        //Enables the behaviour so that the necessary logic is executed
        m_behaviourActive = true;
    }


}
