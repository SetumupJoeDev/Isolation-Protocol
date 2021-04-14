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
    [SerializeField]
    protected Vector2    m_dodgeDirection;
    protected Vector2[]  m_directions;

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
}
