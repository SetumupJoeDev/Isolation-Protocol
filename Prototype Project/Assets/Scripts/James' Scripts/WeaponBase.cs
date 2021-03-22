using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    #region Firing

    [Header("Firing")]
    [SerializeField]
    [Tooltip("The interval between each shot the weapon fires.")]
    protected float m_fireInterval;

    [SerializeField]
    [Tooltip("The projectile that the weapon will fire.")]
    protected GameObject m_projectilePrefab;

    [SerializeField]
    [Tooltip("The number of projectiles fired each time this weapon fires.")]
    protected int m_projectilesPerShot;

    [SerializeField]
    [Tooltip("The sound this weapon makes when it is fired.")]
    protected AudioSource m_fireSound;

    [SerializeField]
    [Tooltip("Determines whether or not the weapon can currently fire.")]
    protected bool m_canWeaponFire;

    [SerializeField]
    [Tooltip("The position on the weapon from which the projectiles are fired.")]
    protected Transform m_barrelTransform;

    [Space]

    #endregion

    #region Reloading & Ammo

    [Header("Reloading & Ammo")]

    [SerializeField]
    [Tooltip("The amount of time it takes for this weapon to reload.")]
    protected float m_reloadTime;

    [SerializeField]
    [Tooltip("The sound this weapon makes when it reloads.")]
    protected AudioSource m_reloadSound;

    [SerializeField]
    [Tooltip("The magazine capacity of this weapon.")]
    protected int m_magCapacity;

    [SerializeField]
    [Tooltip("The current amount of ammo loaded in the weapon.")]
    protected int m_currentMagAmmo;

    [SerializeField]
    [Tooltip("The maximum amount of ammo that can be carried for this weapon.")]
    protected int m_maxAmmoCapacity;

    [SerializeField]
    [Tooltip("The current amount of ammo for this weapon the player is carrying.")]
    protected int m_currentCarriedAmmo;

    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Calculates the direction for the weapon to point based on the positions of the mouse and the object
        Vector2 direction = Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position;

        //Calculates the angle using the x and y of the direction vector, converting it to degrees for use in a quaternion
        float angle = Mathf.Atan2( direction.x, direction.y ) * Mathf.Rad2Deg;

        //Calculates the rotation around the Z axis using the angle
        Quaternion rotation = Quaternion.AngleAxis( angle, Vector3.back );

        //Sets the rotation of the weapon to the rotation calculated above
        transform.rotation = rotation;

    }

    protected virtual void FireWeapon( )
    {
        Instantiate( m_projectilePrefab, m_barrelTransform.position, Quaternion.identity );

        m_fireSound.Play( );

    }

    protected virtual void ReloadWeapon( )
    {

        m_currentCarriedAmmo -= m_magCapacity - m_currentMagAmmo;

        m_currentMagAmmo = m_magCapacity;

        m_reloadSound.Play( );

    }

}
