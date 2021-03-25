using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{

    [Header("Weapons")]
    [Tooltip("The weapon held by the player.")]
    public WeaponBase m_currentWeapon;

    [Header("Animation")]
    public Animator m_animator;
    protected int m_currentLayerIndex = 1;

    [SerializeField]
    protected Vector3 m_direction;

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        m_directionalVelocity.y = Input.GetAxisRaw("Vertical");

        if( Input.GetMouseButtonDown( 0 ) && m_currentWeapon.m_weaponFireMode == WeaponBase.fireModes.semiAuto )
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

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //m_direction = mousePos - transform.position;

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
        Move();
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

}
