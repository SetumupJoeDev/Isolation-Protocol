using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAiming : MonoBehaviour
{

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        Vector3 direction = mousePos - transform.position;

        Vector3 laserTarget = transform.position + ( direction.normalized * 1000 );

        Debug.DrawRay(transform.position, laserTarget , Color.red );

        //if( Input.GetMouseButtonDown( 0 ))
        //{
        //    projectile newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<projectile>();

        //    newBullet.velocity =  mousePos - transform.position ;

        //}

    }
}
