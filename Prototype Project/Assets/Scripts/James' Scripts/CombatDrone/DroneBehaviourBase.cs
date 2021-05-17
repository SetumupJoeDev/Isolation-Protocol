using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourBase : MonoBehaviour
{
    [SerializeField]
    protected DroneController m_droneController;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //Sets the drone controller as the DroneController component of the gameObject
        m_droneController = GetComponent<DroneController>( );
    }

    public virtual void EnableModuleBehaviour( )
    {
        Debug.Log( "Behaviour activated!" );
    }

    public virtual void DisableModuleBehaviour( )
    {
        Debug.Log( "Behaviour deactivated!" );
    }

}
