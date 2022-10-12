using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileList
{
   public  GameObject bulletShot;
   public float timeShot;
   public float timeExpired;
   public Vector3 locationShot;
   public Vector3 locationExpired;


    public void storeData (GameObject bullet, float whenShot, float whenGone, Vector3 whereShot, Vector3 whereGone)
    {
        bullet = bulletShot;
        whenShot = timeShot;
        whenGone = timeExpired;
        whereShot = locationShot;
        whereGone = locationExpired;
    }

}
