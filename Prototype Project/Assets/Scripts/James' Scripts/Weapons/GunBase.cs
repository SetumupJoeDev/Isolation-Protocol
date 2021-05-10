﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    #region Firing

    [Header("Firing")]
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

    protected bool m_hasReloaded;

    [SerializeField]
    protected CanvasController m_reloadingCanvas;

    [SerializeField]
    protected Slider m_reloadingBar;

    #endregion

    #region ShopValues

    [SerializeField]
    private int m_baseShopValue;

    [SerializeField]
    private int m_fabricatorStorePrice;

    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {

        m_magazineUIText = GameObject.Find( "Magazine" ).GetComponent<Text>( );

        m_totalAmmoText = GameObject.Find( "TotalAmmo" ).GetComponent<Text>( );

        UpdateUIElements( );

        m_canWeaponFire = true;

        m_reloadTime = m_reloadSound.clip.length;

        m_reloadingCanvas = GameObject.Find("ReloadingBarCanvas").GetComponent<CanvasController>( );

        m_reloadingBar = GameObject.Find("ReloadingBar").GetComponent<Slider>( );

    }

    protected virtual void Awake( )
    {

        if ( m_magazineUIText != null && m_totalAmmoText != null )
        {
            UpdateUIElements( );
        }
    }

    // Update is called once per frame
    protected virtual void Update( )
    {

        PointToMouse( );

        if( m_reloadSound.isPlaying )
        {
            m_reloadingBar.value += ( ( Time.deltaTime / m_reloadTime ) * 100 );
        }

    }

    public virtual void FireWeapon( )
    {
        //Weapon firing logic goes in here
        Debug.Log( "Weapon fired!" );
    }

    public virtual void ReloadWeapon( )
    {
        if ( m_currentMagAmmo < m_magCapacity )
        {
            //Plays the weapon's reload sound
            m_reloadSound.Play( );

            m_reloadingCanvas.ToggleCanvas( );

            StartCoroutine( ReloadWithDelay( ) );

        }
    }

    public IEnumerator ReloadWithDelay( )
    {
        yield return new WaitForSeconds( m_reloadTime );

        if ( m_currentCarriedAmmo - ( m_magCapacity - m_currentMagAmmo ) > 0 )
        {

            //Reduces the amount of ammo the player is carrying by the difference between the current magazine ammo and the magazine capacity
            m_currentCarriedAmmo -= m_magCapacity - m_currentMagAmmo;

            //Sets the current magazine ammo to its capacity
            m_currentMagAmmo = m_magCapacity;

        }
        else
        {

            m_currentMagAmmo += m_currentCarriedAmmo;

            m_currentCarriedAmmo = 0;
        }

        UpdateUIElements( );

        m_reloadingCanvas.ToggleCanvas( );

        m_reloadingBar.value = 0;

    }

    public virtual void UpdateUIElements( )
    {
        m_totalAmmoText.text = m_currentCarriedAmmo.ToString( );

        m_magazineUIText.text = m_currentMagAmmo.ToString( ) + " / " + m_magCapacity.ToString( );
    }

    protected virtual void PointToMouse( )
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

}
