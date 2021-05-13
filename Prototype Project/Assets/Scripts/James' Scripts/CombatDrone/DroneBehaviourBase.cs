using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourBase : MonoBehaviour
{
    [SerializeField]
    protected DroneController m_droneController;

    // Start is called before the first frame update
    void Awake()
    {
        m_droneController = GetComponent<DroneController>( );
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if( Input.GetKeyDown(KeyCode.Q) )
        {
            m_droneController.DisableBasicBehaviours( );
            EnableModuleBehaviour( );
        }
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
