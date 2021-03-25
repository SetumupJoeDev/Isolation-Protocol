﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{

    [Header("Weapons")]
    [Tooltip("The weapon held by the player.")]
    public WeaponBase    m_currentWeapon;

    [Header("Animation")]
    public Animator      m_animator;
    protected int        m_currentLayerIndex;

    [Header("Dodge")]
    [SerializeField]
    protected Vector3    m_mouseDirection;
    [SerializeField]
    protected float      m_dodgeSpeed;
    protected float      m_dodgeTime;
    [SerializeField]
    protected float      m_startDodgeTime;
    protected bool       m_canDodge;
    protected bool       m_isDodging;

    protected override void Start()
    {
        m_currentLayerIndex = 1;
        m_dodgeTime = m_startDodgeTime;
        m_canDodge = true;
        m_isDodging = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        m_directionalVelocity.y = Input.GetAxisRaw("Vertical");


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_mouseDirection = mousePos - transform.position;

        Dodge();

        if ( Input.GetMouseButtonDown( 0 ) && m_currentWeapon.m_weaponFireMode == WeaponBase.fireModes.semiAuto )
        {
            m_currentWeapon.FireWeapon( );
        }
        else if( Input.GetMouseButton( 0 ) && m_currentWeapon.m_weaponFireMode == WeaponBase.fireModes.fullAuto )
        {
            m_currentWeapon.FireWeapon( );
        }

        if( Input.GetKeyDown(KeyCode.R) )
        {
            m_currentWeapon.ReloadWeapon( );
        }


        if(m_directionalVelocity.x > 0)
        {
            Animate(4);
        }
        if (m_directionalVelocity.x < 0)
        {
            Animate(3);
        }
        if (m_directionalVelocity.y < 0)
        {
            Animate(2);
        }
        if (m_directionalVelocity.y > 0)
        {
            Animate(1);
        }

        //m_animator.SetBool("isWalking", false);
    }

    protected override void FixedUpdate()
    {
        if (m_isDodging)
        {
            m_characterRigidBody.velocity = m_mouseDirection.normalized * m_dodgeSpeed * Time.deltaTime;
        }
        else
        {
            Move();
        }
    }

    public void ReplenishAmmo( )
    {

        m_currentWeapon.m_currentCarriedAmmo = m_currentWeapon.m_maxAmmoCapacity;

        m_currentWeapon.UpdateUIElements( );

    }

    protected void Animate(int layerIndex)
    {
        //m_animator.SetBool("isWalking", true);

        m_animator.SetLayerWeight(m_currentLayerIndex, 0);
        m_animator.SetLayerWeight(layerIndex, 1);

        m_currentLayerIndex = layerIndex;
    }

    protected void Dodge()
    {
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
            }
            else
            {
                m_dodgeTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator DodgeCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        m_canDodge = true;
    }
}
