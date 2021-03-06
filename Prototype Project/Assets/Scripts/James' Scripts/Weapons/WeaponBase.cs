using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    //THIS CLASS IS NOW DEPRECATED

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
    public int m_maxAmmoCapacity;

    [SerializeField]
    [Tooltip("The current amount of ammo for this weapon the player is carrying.")]
    public int m_currentCarriedAmmo;

    [SerializeField]
    [Tooltip("The UI element for displaying the weapon's current ammo.")]
    protected Text m_magazineUIText;

    [SerializeField]
    [Tooltip("The UI element for displaying the weapon's total ammo.")]
    protected Text m_totalAmmoText;

    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {

        UpdateUIElements( );

        m_canWeaponFire = true;

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

    public IEnumerator LaunchProjectiles( )
    {
        
        for ( int i = 0; i < m_projectilesPerShot; i++ )
        {
            //Instantiates a projectile at the barrel of the weapon 
            ProjectileBase newBullet = Instantiate( m_projectilePrefab, m_barrelTransform.position, Quaternion.identity ).GetComponent<ProjectileBase>();

            //Sets the projectiles velocity to the aiming direction of the weapon
            newBullet.m_projectileVelocity = m_aimingDirection;

            m_currentMagAmmo--;

            UpdateUIElements( );

            //Play's the weapon's firing sound
            m_fireSound.Play( );

            //Adds a short delay between firing each projectile, so they aren't all fired at once
            yield return new WaitForSeconds( m_fireInterval );

        }

        m_canWeaponFire = true;

    }

    public virtual void FireWeapon( )
    {
        //If the weapon can fire and has sufficient ammo to do so, the firing coroutine is started
        if ( m_canWeaponFire && m_currentMagAmmo - m_projectilesPerShot >= 0 )
        {
            StartCoroutine( LaunchProjectiles( ) );

            //This is set to false so that the coroutine isn't started more than once before it ends
            m_canWeaponFire = false;

        }

    }

    public virtual void ReloadWeapon( )
    {

        if ( m_currentMagAmmo < m_magCapacity )
        {

            if ( m_currentCarriedAmmo - ( m_magCapacity - m_currentMagAmmo ) > 0 )
            {

                //Reduces the amount of ammo the player is carrying by the difference between the current magazine ammo and the magazine capacity
                m_currentCarriedAmmo -= m_magCapacity - m_currentMagAmmo;

                //Sets the current magazine ammo to its capacity
                m_currentMagAmmo = m_magCapacity;

            }
            else
            {

                //If the player doesn't have enough ammo for a full reload, their current ammo is increased to whatever is left and their carried ammo is set to 0
                m_currentMagAmmo += m_currentCarriedAmmo;

                m_currentCarriedAmmo -= m_currentCarriedAmmo;
            }

            //Plays the weapon's reload sound
            m_reloadSound.Play( );

            //Updates all of the necessary UI elements to reflect the change in values
            UpdateUIElements( );

        }

    }


    public virtual void UpdateUIElements( )
    {
        //Updates the weapon UI elements with the latest values
        m_totalAmmoText.text = m_currentCarriedAmmo.ToString( );

        m_magazineUIText.text = m_currentMagAmmo.ToString( ) + " / " + m_magCapacity.ToString( );
    }


}
