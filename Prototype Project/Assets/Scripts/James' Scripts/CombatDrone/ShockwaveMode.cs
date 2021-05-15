using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMode : DroneBehaviourBase
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

    private bool m_shockwaveActive;

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

        SetUpRenderer( );

    }

    public override void Update( )
    {
        if ( !m_shockwaveActive )
        {
            base.Update( );
        }
        else
        {
            GrowShockwave( );
            DrawShockwave( );
        }
    }

    private void SetUpRenderer( )
    {
        m_shockwaveRenderer = gameObject.AddComponent( typeof( LineRenderer ) ) as LineRenderer;

        m_shockwaveRenderer.loop = true;

        m_shockwaveRenderer.material = m_shockwaveMaterial;

        m_shockwaveRenderer.sortingOrder = 5;

        m_shockwaveRenderer.widthMultiplier = m_shockwaveLineWidth;
    }

    public void GrowShockwave( )
    {
        Collider2D[] affectedEnemies = Physics2D.OverlapCircleAll(transform.position, m_currentShockwaveRadius, m_targetLayer );

        if ( affectedEnemies.Length >= 1 )
        {
            for ( int i = 0; i < affectedEnemies.Length; i++ )
            {
                Debug.Log( "Enemy knocked back!" );
            }
        }

        m_currentShockwaveRadius += m_shockwaveSpeed * Time.deltaTime;

        if ( m_currentShockwaveRadius >= m_shockwaveMaxRadius )
        {

            m_droneController.EnableBasicBehaviours( );

            m_currentShockwaveRadius = 0.0f;

            m_shockwaveRenderer.enabled = false;

            m_shockwaveActive = false;
        }
    }

    public void DrawShockwave( )
    {

        float deltaTheta = (2f * Mathf.PI) / m_shockwaveVertCount;
        float theta = 0.0f;

        m_shockwaveRenderer.positionCount = m_shockwaveVertCount;

        for( int i = 0; i < m_shockwaveRenderer.positionCount; i++ )
        {
            Vector3 pos = new Vector3( m_currentShockwaveRadius * Mathf.Cos( theta ) + transform.position.x , m_currentShockwaveRadius * Mathf.Sin( theta ) + transform.position.y , 0.0f );
            m_shockwaveRenderer.SetPosition( i , pos );
            theta += deltaTheta;
        }

    }

    public override void EnableModuleBehaviour( )
    {
        m_droneController.DisableBasicBehaviours( );

        m_shockwaveRenderer.enabled = true;

        m_shockwaveActive = true;
    }


}
