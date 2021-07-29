using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventListener : MonoBehaviour
{
    
    public static TutorialEventListener current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnDummyHit;

    public event Action OnAimTaskStart;

    public event Action OnAimTaskEnd;

    public event Action OnHoloSlugDeath;

    public event Action OnGrappleTaskStart;

    public void DummyHit()
    {
        if( OnDummyHit != null)
        {
            OnDummyHit( );
        }
    }

    public void AimTaskStart()
    {
        if(OnAimTaskStart != null)
        {
            OnAimTaskStart( );
        }
    }

    public void AimTaskEnd()
    {
        if( OnAimTaskEnd != null)
        {
            OnAimTaskEnd( );
        }
    }

    public void HoloSlugDeath()
    {
        if(OnHoloSlugDeath != null)
        {
            OnHoloSlugDeath( );
        }
    }

    public void GrappleTaskStart()
    {
        if( OnGrappleTaskStart != null )
        {
            OnGrappleTaskStart( );
        }
    }

}
