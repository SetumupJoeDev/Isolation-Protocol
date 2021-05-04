using System.Collections;
using UnityEngine;

public class PlayerController : CharacterBase
{

    [Header("Weapons")]

    [Tooltip("The position of the weapon on the player.")]
    public GameObject   m_weaponAttachPoint;

    //James' Work

    [Tooltip("The weapon held by the player.")]
    public GameObject   m_currentWeapon;

    [Tooltip("The array index of the player's currently equipped weapon.")]
    public int          m_currentWeaponIndex;

    [Tooltip("The array containing the player's carried weapons.")]
    public GameObject[] m_carriedWeapons;

    //End of James' work

    [Header("Animation")]
    public Animator      m_animator;

    [Header("HUD")]
    public HUDManager    m_hud;

    [Header("Dash")]
    public float         m_dashCooldown;
    
    [SerializeField]
    protected Vector2    m_mouseDirection;
    [SerializeField]
    protected float      m_dashSpeed;
    protected float      m_dashTime;
    [SerializeField]
    protected float      m_startDashTime;
    protected float      m_directionTolerance;
    [HideInInspector]
    public bool          m_canDash;
    [HideInInspector]
    public bool          m_isDashing;
    protected Vector2    m_dashDirection;
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

    #region Knockback

    [Header("Knockback")]

    [Tooltip("The duration of the knockback effect.")]
    public float m_knockbackDuration;

    [Tooltip("The direction in which the player has been knocked back.")]
    public Vector3 m_knockbackDirection;

    [Tooltip("The amount of force with which the player has been knocked back.")]
    public float m_knockbackForce;

    [Tooltip("Determines whether or not the player is currently being knocked back.")]
    public bool m_knockedBack;

    #endregion

    #region Currency

    public CurrencyManager m_currencyManager;

    public LayerMask m_interactiveLayer;

    public float m_interactionRange;

    #endregion

    #region AudioLogs

    [Header("AudioLogs")]

    public AudioLogListController m_audioLogList;

    public CanvasController m_audioLogCanvas;

    #endregion

    //End of James' work

    protected override void Start()
    {
        m_dashTime = m_startDashTime;
        m_canDash  = true;
        m_isDashing = false;
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
        Dash();

        CheckForInteractables( );

        if ( Input.GetMouseButton( 0 ) )
        {
            m_currentWeapon.GetComponent<GunBase>().FireWeapon( );
        }

        float scrollWheelValue = Input.GetAxis( "Mouse ScrollWheel" );

        if ( scrollWheelValue != 0 )
        {
            float indexModifier = scrollWheelValue * 10;
            SwapWeapon( (int)indexModifier );
        }

        if( Input.GetKeyDown(KeyCode.R) )
        {
            m_currentWeapon.GetComponent<GunBase>( ).ReloadWeapon( );
        }

        if ( Input.GetKeyDown( KeyCode.Tab ) )
        {
            m_audioLogCanvas.ToggleCanvas( );
        }

    }

    protected override void FixedUpdate()
    {
        if (m_isDashing && !m_knockedBack )
        {
            m_characterRigidBody.velocity = m_dashDirection.normalized * m_dashSpeed * Time.deltaTime;

            //James' work

            if( Physics2D.Raycast( transform.position, m_dashDirection, 1.0f, m_wallLayer ) == true )
            {
                KillAttachedSlugs( );
            }

        }
        else if( m_knockedBack )
        {
            m_characterRigidBody.velocity = m_knockbackDirection.normalized * m_knockbackForce * Time.deltaTime;
        }
        //End of James' work
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

        //for (int i = 0; i < m_animator.GetCurrentAnimatorClipInfo(0).Length; i++)
        //{
            string clipName = m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;


            if (clipName == "IdleRight" || clipName == "WalkRight")
            {
                m_weaponAttachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
                m_currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (clipName == "IdleLeft" || clipName == "WalkLeft")
            {
                m_weaponAttachPoint.transform.localPosition = new Vector3(0f, -0.3f, 0f);
                m_currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            else if (clipName == "IdleUp" || clipName == "WalkUp")
            {
                m_weaponAttachPoint.transform.localPosition = new Vector3(-0.3f, -0.25f, 0f);
                m_currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if (clipName == "IdleDown" || clipName == "WalkDown")
            {
                m_weaponAttachPoint.transform.localPosition = new Vector3(0.3f, -0.25f, 0f);
                m_currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        //}
    }

    protected void Dash()
    {
        for (int i = 0; i < m_directions.Length; i++)
        {
            if (Mathf.Abs(m_directionalVelocity.normalized.x - m_directions[i].x) < m_directionTolerance && Mathf.Abs(m_directionalVelocity.normalized.y - m_directions[i].y) < m_directionTolerance)
            {
                m_dashDirection = m_directions[i];
            }
        }

        if (m_canDash)
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_isDashing = true;
                m_canDash = false;
                m_healthManager.m_isInvulnerable = true;
            }
        }

        if (m_isDashing)
        {
            if (m_dashTime <= 0)
            {
                m_isDashing = false;
                m_dashTime = m_startDashTime;
                m_characterRigidBody.velocity = Vector2.zero;
                m_dashDirection = Vector2.zero;
                m_healthManager.m_isInvulnerable = false;
                m_hud.DashCooldown();
                StartCoroutine(DashCooldown());
            }
            else
            {
                m_dashTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(m_dashCooldown);
        m_canDash = true;
        m_hud.m_dashOnCooldown = false;
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

    public void SwapWeapon( int indexModifier )
    {

        int newIndex = m_currentWeaponIndex + indexModifier;

        if( newIndex < 0 )
        {
            newIndex = m_carriedWeapons.Length - 1;
        }
        if( newIndex >= m_carriedWeapons.Length )
        {
            newIndex = 0;
        }

        if ( m_carriedWeapons[newIndex] != null )
        {
            m_currentWeapon.SetActive( false );

            m_currentWeapon = m_carriedWeapons[newIndex];

            m_currentWeaponIndex = newIndex;

            m_currentWeapon.SetActive( true );
        }

    }

    public IEnumerator KnockBack( )
    {
        m_knockedBack = true;

        yield return new WaitForSeconds( m_knockbackDuration );

        m_knockedBack = false;

    }

    public void CheckForInteractables( )
    {

        RaycastHit2D interactableObject = Physics2D.Raycast(transform.position, m_mouseDirection, m_interactionRange, m_interactiveLayer);

        Debug.DrawRay( transform.position , m_mouseDirection * m_interactionRange );

        if ( interactableObject )
        {
            InteractableObject interactable = interactableObject.collider.gameObject.GetComponent<InteractableObject>( );

            interactable.m_isBeingLookedAt = true;

            interactable.m_playerController = this;

            if ( Input.GetKeyDown( KeyCode.E ) )
            {
                interactable.Activated( );
            }
        }

        

    }
    //End of James' work

}
