using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{

    [Header("Weapons")]
    [Tooltip("The weapon held by the player.")]
    public GameObject   m_currentWeapon;

    [Header("Animation")]
    public Animator      m_animator;

    [Header("Dodge")]
    [SerializeField]
    protected Vector2    m_mouseDirection;
    [SerializeField]
    protected float      m_dodgeSpeed;
    protected float      m_dodgeTime;
    [SerializeField]
    protected float      m_startDodgeTime;
    protected float      m_directionTolerance;
    protected bool       m_canDodge;
    protected bool       m_isDodging;
    protected Vector2    m_dodgeDirection;
    protected Vector2[]  m_directions;
    public float         m_slowness;

    //James' Work
    #region MutoSlug Attachments

    [Header("MutoSlug Attachments")]

    [Tooltip("An array of GameObjects that MutoSlugs can attach to.")]
    public GameObject[] m_slugAttachmentPoints;

    [Tooltip("Determines whether or not the player currently has any slugs attached to them.")]
    public bool m_hasAttachedSlugs;

    [Tooltip("The physics layer that the walls sit on, used to detect collisions during dodges.")]
    public LayerMask m_wallLayer;

    #endregion

    //End of James' work

    protected override void Start()
    {
        m_dodgeTime = m_startDodgeTime;
        m_canDodge  = true;
        m_isDodging = false;
        m_directionTolerance = 0.5f;
        m_directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(-1, 1),     // up-left
            new Vector2(1, 1),      // up-right
            new Vector2(-1, -1),    // down-left
            new Vector2(1, -1)      // down-right
        };
    }

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        m_directionalVelocity.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        m_mouseDirection = mousePos - transform.position;

        Animate();
        Dodge();

        if ( Input.GetMouseButtonDown( 0 ) )
        {
            m_currentWeapon.GetComponent<GunBase>().FireWeapon( );
        }
        

        if( Input.GetKeyDown(KeyCode.R) )
        {
            m_currentWeapon.GetComponent<GunBase>( ).ReloadWeapon( );
        }
    }

    protected override void FixedUpdate()
    {
        if (m_isDodging)
        {
            m_characterRigidBody.velocity = m_dodgeDirection.normalized * m_dodgeSpeed * Time.deltaTime;

            //James' work

            if( Physics2D.Raycast( transform.position, m_dodgeDirection, 1.0f, m_wallLayer ) == true )
            {
                KillAttachedSlugs( );
            }

            //End of James' work

        }
        else
        {
            Move();
        }
    }

    public void ReplenishAmmo( )
    {

        GunBase gunBase = m_currentWeapon.GetComponent<GunBase>();

        gunBase.m_currentCarriedAmmo = gunBase.m_maxAmmoCapacity;

        gunBase.UpdateUIElements( );

    }

    protected void Animate()
    {
        m_animator.SetFloat("Horizontal", m_mouseDirection.x);
        m_animator.SetFloat("Vertical", m_mouseDirection.y);
        m_animator.SetFloat("Speed", m_directionalVelocity.sqrMagnitude);
    }

    protected void Dodge()
    {
        for (int i = 0; i < m_directions.Length; i++)
        {
            if (Mathf.Abs(m_directionalVelocity.normalized.x - m_directions[i].x) < m_directionTolerance && Mathf.Abs(m_directionalVelocity.normalized.y - m_directions[i].y) < m_directionTolerance)
            {
                m_dodgeDirection = m_directions[i];
            }
        }

        if (m_canDodge)
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_isDodging = true;
                m_canDodge = false;
                m_healthManager.m_isInvulnerable = true;
            }
        }

        if (m_isDodging)
        {
            if (m_dodgeTime <= 0)
            {
                StartCoroutine(DodgeCooldown());
                m_isDodging = false;
                m_dodgeTime = m_startDodgeTime;
                m_characterRigidBody.velocity = Vector2.zero;
                m_dodgeDirection = Vector2.zero;
                m_healthManager.m_isInvulnerable = false;
            }
            else
            {
                m_dodgeTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator DodgeCooldown()
    {
        yield return new WaitForSeconds(0.75f);
        m_canDodge = true;
    }

    protected override void Move()
    {
        m_characterRigidBody.velocity = m_directionalVelocity.normalized * (m_moveSpeed + m_slowness) * Time.fixedDeltaTime;
    }

    //James' Work
    public bool AttachNewSlug( GameObject newSlug )
    {

        //Local boolean used to determine whether or not this slug has been attached to the player
        bool hasAttached = false;

        //Loops through all of the attachment points on the player. If one is free, the slug is attached to it and hasAttached is set to true
        foreach( GameObject attachPoint in m_slugAttachmentPoints )
        {
            if( attachPoint.transform.childCount == 0 )
            {
                newSlug.transform.parent = attachPoint.transform;

                hasAttached = true;

                m_hasAttachedSlugs = true;

            }
        }

        //Returns the value of hasAttached so that the slug can determine whether or not to run the next set of logic
        return hasAttached;

    }

    private void KillAttachedSlugs( )
    {
        //Loops through each attachment point in the array, destroying the children of any of them
        foreach( GameObject attachPoint in m_slugAttachmentPoints )
        {
            if( attachPoint.transform.childCount != 0 )
            {
                //Kills the slug attached to this point
                attachPoint.transform.GetChild( 0 ).gameObject.GetComponent<MutoSlug>( ).Die( );
            }
        }
    }

    //End of James' work

}
