using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    #region Shockwave Drawing

    [Header("Shockwave Drawing")]

    public LineRenderer m_shockwaveRenderer;

    public int m_shockwaveVertCount;

    public float m_shockwaveLineWidth;

    public Material m_shockwaveMaterial;

    [Space]

    #endregion

    #region Shockwave Logic

    [Header("Shockwave Attack")]

    public float m_knockbackForce;

    public int m_shockwaveDamage;

    public float m_knockbackDuration;

    public float m_shockwaveMaxRadius;

    public float m_shockwaveSpeed;

    public float m_currentShockwaveRadius;

    public LayerMask m_targetLayer;

    public bool m_shockwaveEnded;

    public bool m_shockwaveActive;

    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {

        SetUpRenderer( );

        m_shockwaveActive = true;

    }

    private void OnDisable( )
    {
        Destroy( m_shockwaveRenderer );
    }

    // Update is called once per frame
    void Update( )
    {
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

                float distanceToTarget = Vector3.Distance(transform.position, affectedTargets[i].transform.position);

                if ( affectedTargets[i].GetComponent<HealthManager>( ).m_isVulnerable && distanceToTarget >= m_currentShockwaveRadius )
                {

                    StartCoroutine( affectedTargets[i].GetComponent<CharacterBase>( ).KnockBack( m_knockbackForce , m_knockbackDuration , gameObject ) );

                    affectedTargets[i].gameObject.GetComponent<HealthManager>( ).TakeDamage( m_shockwaveDamage );

                }

            }
        }

        //Increases the radius of the shockwave by shockwaveSpeed multiplied by deltaTime, so that the shockwave grows over time
        m_currentShockwaveRadius += m_shockwaveSpeed * Time.deltaTime;

        //If the radius is greater than or equal to the maximum radius, then the shockwave stops
        if ( m_currentShockwaveRadius >= m_shockwaveMaxRadius )
        {
            //Disable the effect here
            
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
