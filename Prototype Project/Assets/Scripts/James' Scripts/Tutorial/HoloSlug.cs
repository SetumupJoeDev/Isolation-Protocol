using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloSlug : MutoSlug
{

    public override void Die()
    {
        base.Die();
        TutorialEventListener.current.HoloSlugDeath( );
    }

}
