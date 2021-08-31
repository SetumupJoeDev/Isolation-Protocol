using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventListener : MonoBehaviour
{
    
    //The current instance of the event listener
    public static TutorialEventListener current;

    private void Awake()
    {
        //Sets current as this instance of the script
        current = this;
    }

    //The event that gets called when a tutorial dummy is shot
    public event Action OnDummyHit;

    //The event that gets called when the aiming task starts
    public event Action OnAimTaskStart;

    //The event that gets called when the aiming task ends
    public event Action OnAimTaskEnd;

    //The event that gets called when the Holo Slug is killed
    public event Action OnHoloSlugDeath;

    //The event that gets called when the grapple counter task starts
    public event Action OnGrappleTaskStart;

    public void DummyHit()
    {
        //Calls the event if any methods are subscribed to it
        if( OnDummyHit != null)
        {
            OnDummyHit( );
        }
    }

    public void AimTaskStart()
    {
        //Calls the event if any methods are subscribed to it
        if ( OnAimTaskStart != null)
        {
            OnAimTaskStart( );
        }
    }

    public void AimTaskEnd()
    {
        //Calls the event if any methods are subscribed to it
        if ( OnAimTaskEnd != null)
        {
            OnAimTaskEnd( );
        }
    }

    public void HoloSlugDeath()
    {
        //Calls the event if any methods are subscribed to it
        if ( OnHoloSlugDeath != null)
        {
            OnHoloSlugDeath( );
        }
    }

    public void GrappleTaskStart()
    {
        //Calls the event if any methods are subscribed to it
        if ( OnGrappleTaskStart != null )
        {
            OnGrappleTaskStart( );
        }
    }

}
