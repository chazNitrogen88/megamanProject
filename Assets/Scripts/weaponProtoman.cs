using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponProtoman : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        // if(Input.GetKey("q"))
        {
            Shoot();
        }
        
    }
    void Shoot()
    {
       Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
