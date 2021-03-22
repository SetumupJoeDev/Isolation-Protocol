using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [Header("Firing")]
    [SerializeField]
    [Tooltip("The interval between each shot the weapon fires.")]
    protected float m_fireInterval;

    [SerializeField]
    [Tooltip("The number of projectiles fired each time this weapon fires.")]
    protected int m_projectilesPerShot;

    [SerializeField]
    [Tooltip("Determines whether or not the weapon can currently fire.")]
    protected bool m_canWeaponFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
