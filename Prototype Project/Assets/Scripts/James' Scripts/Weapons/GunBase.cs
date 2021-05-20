using System.Collections;
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
    protected uint m_magCapacity;

    [SerializeField]
    [Tooltip("The current amount of ammo loaded in the weapon.")]
    protected uint m_currentMagAmmo;

    [SerializeField]
    [Tooltip("The maximum amount of ammo that can be carried for this weapon.")]
    public uint m_maxAmmoCapacity;

    [SerializeField]
    [Tooltip("The current amount of ammo for this weapon the player is carrying.")]
    public uint m_currentCarriedAmmo;

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

        //Finds and assigns the magazine text UI gameobject for use in displaying the values to the player
        m_magazineUIText = GameObject.Find( "Magazine" ).GetComponent<Text>( );

        //Finds and assigns the total ammo text UI gameobject for use in displaying the values to the player
        m_totalAmmoText = GameObject.Find( "TotalAmmo" ).GetComponent<Text>( );

        //Updates the newly assigned UI elements
        UpdateUIElements( );

        //Sets the weapon to be able to fire on being awake
        m_canWeaponFire = true;

        //Sets the duration of the reload time to be the duration of the reloading soundclip
        m_reloadTime = m_reloadSound.clip.length;

        //Finds and assigns the reloading bar UI canvas for use in displaying reload progress to the player
        m_reloadingCanvas = GameObject.Find("ReloadingBarCanvas").GetComponent<CanvasController>( );

        //Finds and assigns the reloading bar UI slider for use in displaying reload progress to the player
        m_reloadingBar = GameObject.Find("ReloadingBar").GetComponent<Slider>( );

    }

    protected virtual void Awake( )
    {
        //Updates the UI elements to reflect the newly equipped weapon
        if ( m_magazineUIText != null && m_totalAmmoText != null )
        {
            UpdateUIElements( );
        }
    }

    // Update is called once per frame
    protected virtual void Update( )
    {
        //Points the weapon towards the player's mouse cursor
        PointToMouse( );

        //If the reloading sound is playing, the reload bar's value is increased by the percentage of time that has passed since the last update
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
        if ( m_currentMagAmmo < m_magCapacity && m_currentCarriedAmmo > 0 )
        {
            //Plays the weapon's reload sound
            m_reloadSound.Play( );

            //Toggles the reloading canvas on to 
            m_reloadingCanvas.ToggleCanvas( );

            //Starts the reloading coroutine to reload over a course of time
            StartCoroutine( ReloadWithDelay( ) );

        }
    }

    public IEnumerator ReloadWithDelay( )
    {
        yield return new WaitForSeconds( m_reloadTime );

        if ( (int)m_currentCarriedAmmo - ( (int)m_magCapacity - m_currentMagAmmo ) > 0 )
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

            m_currentCarriedAmmo = 0;
        }

        UpdateUIElements( );

        //Toggles the reloading bar canvas to turn it back off
        m_reloadingCanvas.ToggleCanvas( );

        //Resets the value of the reload bar so it can be used again next time
        m_reloadingBar.value = 0;

    }

    public virtual void UpdateUIElements( )
    {

        //Updates the text value of the TotalAmmo UI element to reflect the current value of currentCarriedAmmo
        m_totalAmmoText.text = m_currentCarriedAmmo.ToString( );

        //Updates the text value of the Magazine UI element to reflect the current value of currentMagAmmo
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

    }

}
