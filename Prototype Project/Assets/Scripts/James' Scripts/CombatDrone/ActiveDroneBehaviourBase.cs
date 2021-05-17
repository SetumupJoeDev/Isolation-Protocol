using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActiveDroneBehaviourBase : DroneBehaviourBase
{

    #region Cooldown

    [Header("Cooldown")]

    [SerializeField]
    protected bool m_isInCooldown;

    [SerializeField]
    protected float m_cooldownDuration;

    [SerializeField]
    protected Image m_cooldownOverlay;

    #endregion

    [SerializeField]
    protected bool m_behaviourActive;

    // Update is called once per frame
    public virtual void Update( )
    {
        //If the player presses Q and the behaviour is not active or in cooldown, it is activated
        if ( Input.GetKeyDown( KeyCode.Q ) && !m_isInCooldown && !m_behaviourActive )
        {
            ActivateEffect( );
        }
        //Otherwise, if it is in cooldown, the cooldownOverlay's fill amount is incremented by the percentage of the cooldown duration that's passed since the last frame
        if ( m_isInCooldown )
        {
            m_cooldownOverlay.fillAmount -= Time.deltaTime / m_cooldownDuration;
        }
    }

    public virtual IEnumerator CooldownTimer( )
    {
        //Sets isInCooldown to true and that cooldownOverlay fill amount to 1 to visualise that the ability is in cooldown
        m_isInCooldown = true;

        m_cooldownOverlay.fillAmount = 1;

        //Waits for the cooldown duration to end before setting isInCooldown to false and the fill amount to 0 to visualise that the ability can be used again
        yield return new WaitForSeconds( m_cooldownDuration );

        m_isInCooldown = false;

        m_cooldownOverlay.fillAmount = 0;

    }

    public virtual void ActivateEffect( )
    {
        //Disables the drone's basic behaviours so that it doesn't follow the player or fire upon enemies while the behaviour is active
        m_droneController.DisableBasicBehaviours( );
        m_behaviourActive = true;
        EnableModuleBehaviour( );
    }

}
