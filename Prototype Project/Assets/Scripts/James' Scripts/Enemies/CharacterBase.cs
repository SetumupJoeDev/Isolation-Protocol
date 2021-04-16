using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField]
    [Tooltip("The speed at which the character moves.")]
    protected float m_moveSpeed;

    [SerializeField]
    [Tooltip("The directional velocity of the character. Should be normalised when used to ensure move speed is accurate.")]
    protected Vector2 m_directionalVelocity;

    [SerializeField]
    [Tooltip("The rigidbody component attached to the character. Used to add physical forces.")]
    protected Rigidbody2D m_characterRigidBody;

    [Space]

    [Header("Health Management")]

    [SerializeField]
    [Tooltip("The Health Manager script for this character.")]
    protected HealthManager m_healthManager;

    // Start is called before the first frame update
    protected virtual void Start( )
    {
        
    }

    // Update is called once per frame
    protected virtual void Update( )
    {


    }


    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void Move( )
    {
        m_characterRigidBody.velocity = m_directionalVelocity.normalized * m_moveSpeed * Time.fixedDeltaTime;
    }

}
