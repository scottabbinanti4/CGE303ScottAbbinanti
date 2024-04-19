using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectilePrefab;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Destroy(firedProjectile, 3f);
    }
}
