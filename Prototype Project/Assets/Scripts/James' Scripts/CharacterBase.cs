using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField]
    [Tooltip("The speed at which the character moves.")]
    protected float m_moveSpeed;

    [Tooltip("The directional velocity of the character. Should be normalised when used to ensure move speed is accurate.")]
    protected Vector2 m_directionalVelocity;

    [SerializeField]
    [Tooltip("The rigidbody component attached to the character. Used to add physical forces.")]
    protected Rigidbody2D m_characterRigidBody;

    // Start is called before the first frame update
    protected virtual void Start( )
    {
        
    }

    // Update is called once per frame
    protected virtual void Update( )
    {

        //m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        //m_directionalVelocity.y = Input.GetAxisRaw("Vertical");

    }


    protected virtual void FixedUpdate()
    {
        Move( );
    }

    protected virtual void Move( )
    {
        m_characterRigidBody.velocity = m_directionalVelocity.normalized * m_moveSpeed * Time.fixedDeltaTime;
    }

}
