using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloSlug : MutoSlug
{

    public override void Die()
    {
        //Runs the MutoSlug's base death code
        base.Die();
        //Calls the HoloSlugDeath event so that the GrappleCounter task can be completed
        TutorialEventListener.current.HoloSlugDeath( );
    }

}
