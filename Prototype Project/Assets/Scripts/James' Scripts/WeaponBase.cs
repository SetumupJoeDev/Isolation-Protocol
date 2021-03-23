using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    #region Firing

    public enum fireModes { semiAuto, fullAuto };

    [Header("Firing")]
    [SerializeField]
    [Tooltip("The fire mode of the weapon.")]
    public fireModes m_weaponFireMode;

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

    [SerializeField]
    [Tooltip("The direction in which the weapon is being aimed.")]
    protected Vector3 m_aimingDirection;

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
        m_aimingDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position;

        //Calculates the angle using the x and y of the direction vector, converting it to degrees for use in a quaternion
        float angle = Mathf.Atan2( m_aimingDirection.x, m_aimingDirection.y ) * Mathf.Rad2Deg;

        //Calculates the rotation around the Z axis using the angle
        Quaternion rotation = Quaternion.AngleAxis( angle, Vector3.back );

        //Sets the rotation of the weapon to the rotation calculated above
        transform.rotation = rotation;

        Vector3 laserTarget = m_barrelTransform.position + ( m_aimingDirection.normalized * 1000 );

        Debug.DrawRay( m_barrelTransform.position , laserTarget , Color.red );

    }

    public virtual void FireWeapon( )
    {

        //Instantiates a projectile at the barrel of the weapon 
        projectile newBullet = Instantiate( m_projectilePrefab, m_barrelTransform.position, Quaternion.identity ).GetComponent<projectile>();

        //Sets the projectiles velocity to the aiming direction of the weapon
        newBullet.velocity = m_aimingDirection;

        //Play's the weapon's firing sound
        m_fireSound.Play( );

    }

    public virtual void ReloadWeapon( )
    {

        //Reduces the amount of ammo the player is carrying by the difference between the current magazine ammo and the magazine capacity
        m_currentCarriedAmmo -= m_magCapacity - m_currentMagAmmo;

        //Sets the current magazine ammo to its capacity
        m_currentMagAmmo = m_magCapacity;

        //Plays the weapon's reload sound
        m_reloadSound.Play( );

    }

}
