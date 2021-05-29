using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMegaman : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        // if(Input.GetKey("."))
        {
            Shoot();
        }
        
    }
    void Shoot()
    {
       Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
