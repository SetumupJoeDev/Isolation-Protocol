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
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        Debug.DrawRay(transform.position, mousePos * 1000 , Color.red );

        if( Input.GetMouseButtonDown( 0 ))
        {
            projectile newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<projectile>();

            newBullet.velocity =  Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position ;

        }

    }
}
