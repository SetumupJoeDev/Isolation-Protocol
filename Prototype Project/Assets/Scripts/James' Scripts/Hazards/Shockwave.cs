using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    #region Shockwave Drawing

    [Header("Shockwave Drawing")]

    [Tooltip("The line renderer that represents the size of the shockwave.")]
    public LineRenderer m_shockwaveRenderer;

    [Tooltip("The number of vertices in the line renderer. The more vertices, the smoother the circle.")]
    public int m_shockwaveVertCount;

    [Tooltip("The width of the shcokwave's line.")]
    public float m_shockwaveLineWidth;

    [Tooltip("The material applied to the shockwave line renderer.")]
    public Material m_shockwaveMaterial;

    [Space]

    #endregion

    #region Shockwave Logic

    [Header("Shockwave Attack")]

    [Tooltip("The amount of force applied to whatever the shockwave knocks back.")]
    public float m_knockbackForce;

    [Tooltip("The amount of damage dealt by the shockwave upon contact.")]
    public int m_shockwaveDamage;

    [Tooltip("The time for which the shockwave knocks back its targets.")]
    public float m_knockbackDuration;

    [Tooltip("The maximum radius that the shockwave can reach.")]
    public float m_shockwaveMaxRadius;

    [Tooltip("The speed at which the shockwave grows.")]
    public float m_shockwaveSpeed;

    [Tooltip("The current radius of the shockwave.")]
    public float m_currentShockwaveRadius;

    [Tooltip("The physics layer that the shockwave will use to check for targets.")]
    public LayerMask m_targetLayer;

    [Tooltip("A boolean that determines whether the shockwave has recently occurred and ended.")]
    public bool m_shockwaveEnded;

    [Tooltip("A boolean that determines whether or not the shockwave is currently active.")]
    public bool m_shockwaveActive;

    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {

        //Applies all necessary variables to the line renderer
        SetUpRenderer( );

        //Sets the shockwave as active when the script is enabled
        m_shockwaveActive = true;

    }

    private void OnDisable( )
    {
        //Destroyed the line renderer component as it is no longer needed
        Destroy( m_shockwaveRenderer );
    }

    // Update is called once per frame
    void Update( )
    {
        //If the shockwave is active, it is rendered and grown
        if ( m_shockwaveActive )
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
        Collider2D[] affectedTargets = Physics2D.OverlapCircleAll(transform.position, m_currentShockwaveRadius, m_targetLayer );

        //Checks to see if there are any enemies in the array
        if ( affectedTargets.Length >= 1 )
        {
            //Loops through the array of enemies, starting their knockback coroutines, and damaging them
            for ( int i = 0; i < affectedTargets.Length; i++ )
            {

                //Calculates the distance between the player and the epicenter of the shockwave
                float distanceToTarget = Vector3.Distance(transform.position, affectedTargets[i].transform.position);

                //If the current affected target has a health manager and is within the width of the shockwave, it is knocked back and takes damage
                if ( affectedTargets[i].GetComponent<HealthManager>( ).m_isVulnerable && distanceToTarget >= m_currentShockwaveRadius )
                {

                    //Starts the affected target's knockback coroutine
                    StartCoroutine( affectedTargets[i].GetComponent<CharacterBase>( ).KnockBack( m_knockbackForce , m_knockbackDuration , gameObject ) );

                    //Damages the affected target
                    affectedTargets[i].gameObject.GetComponent<HealthManager>( ).TakeDamage( m_shockwaveDamage );

                }

            }
        }

        //Increases the radius of the shockwave by shockwaveSpeed multiplied by deltaTime, so that the shockwave grows over time
        m_currentShockwaveRadius += m_shockwaveSpeed * Time.deltaTime;

        //If the radius is greater than or equal to the maximum radius, then the shockwave stops
        if ( m_currentShockwaveRadius >= m_shockwaveMaxRadius )
        {
            //Disables the effects of the shockwave
            
            m_shockwaveRenderer.enabled = false;

            m_shockwaveActive = false;

            m_shockwaveEnded = true;

            m_currentShockwaveRadius = 0.0f;

            this.enabled = false;

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
        for ( int i = 0; i < m_shockwaveRenderer.positionCount; i++ )
        {
            //Positions the current vertex using the values previously calculated, adding the transform positions so that they follow the drone
            Vector3 pos = new Vector3( m_currentShockwaveRadius * Mathf.Cos( theta ) + transform.position.x , m_currentShockwaveRadius * Mathf.Sin( theta ) + transform.position.y , 0.0f );
            m_shockwaveRenderer.SetPosition( i , pos );

            //Increases the value of theta by the distance between points
            theta += deltaTheta;
        }

    }

}
