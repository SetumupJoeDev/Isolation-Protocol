using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    #region Movement

    [Header("Movement")]

    [SerializeField]
    [Tooltip("The speed at which the character moves.")]
    public float m_moveSpeed;

    [SerializeField]
    [Tooltip("The directional velocity of the character. Should be normalised when used to ensure move speed is accurate.")]
    public Vector2 m_directionalVelocity;

    [SerializeField]
    [Tooltip("The rigidbody component attached to the character. Used to add physical forces.")]
    public Rigidbody2D m_characterRigidBody;

    [Space]

    #endregion

    #region Health Management

    [Header("Health Management")]

    [SerializeField]
    [Tooltip("The Health Manager script for this character.")]
    protected HealthManager m_healthManager;

    #endregion

    #region Knockback

    [Header("Knockback")]

    [Tooltip("The duration of the knockback effect.")]
    public float m_knockbackDuration;

    [Tooltip("The direction in which the player has been knocked back.")]
    public Vector3 m_knockbackDirection;

    [SerializeField]
    protected float m_knockbackForce;

    [Tooltip("Determines whether or not the player is currently being knocked back.")]
    public bool m_knockedBack;

    #endregion

    // Adam's code
    #region Status Effects

    [Header("Status Effects")]

    [Tooltip("The amount by which the player is slowed")]
    public float    m_slowness;
    
    [SerializeField]
    [Tooltip("Whether or not character is stunned")]
    protected bool  m_stunned;
    [SerializeField]
    [Tooltip("Amount of time character is stunned for in seconds")]
    protected float m_stunDuration;
    [SerializeField]
    [Tooltip("Sprite that appears while character is stunned")]
    protected GameObject m_stunIcon;

    #endregion
    // End of Adam's code

    protected virtual void FixedUpdate()
    {
        //If the character is currently knocked back, then their knockback force is simulated
        if (m_knockedBack)
        {
            SimulateKnockback();
        }
    }

    public void SimulateKnockback()
    {
        //Moves the character using the directional vector knockbackDirection and the speed of knockbackForce
        m_characterRigidBody.velocity = m_knockbackDirection.normalized * m_knockbackForce * Time.deltaTime;
    }

    public IEnumerator KnockBack(float knockbackForce, float knockbackDuration, GameObject knockbackSource)
    {
        //Sets the value of knockbackDirection using the value passed in
        m_knockbackDirection = transform.position - knockbackSource.transform.position;

        //Sets the value of knockbackForce using the value passed in
        m_knockbackForce = knockbackForce;

        //Sets the value of knockbackDuration using the value passed in
        m_knockbackDuration = knockbackDuration;

        //Sets the character as being knocked back
        m_knockedBack = true;

        //Waits for the duration of the knockback before setting the character's velocity to 0 and setting them as no longer being knocked back
        yield return new WaitForSeconds(m_knockbackDuration);

        m_characterRigidBody.velocity = Vector3.zero;

        m_knockedBack = false;

    }

    public virtual void Die()
    {
        //Death logic goes here!
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        if (!m_stunned)
        {
            //Moves the character by their directional velocity and speed
            m_characterRigidBody.velocity = m_directionalVelocity.normalized * (m_moveSpeed + m_slowness) * Time.fixedDeltaTime;
        }
    }

    // Adam's code
     public IEnumerator Stun()
    {
        m_characterRigidBody.velocity = Vector3.zero;
        m_stunned = true;
        m_stunIcon.SetActive(true);

        yield return new WaitForSeconds(m_stunDuration);
        
        m_stunned = false;
        m_stunIcon.SetActive(false);
    }
    // End of Adam's code
}