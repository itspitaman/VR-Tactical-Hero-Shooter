using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
    [Header("References")]
    public GameObject projectile;
    public Transform firePoint;

    [Header("Pistol")]
    public float projectileSpeed;
    public int pistolDamage;

    public void Shoot()
    {
        GameObject spawnedProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        spawnedProjectile.GetComponent<Rigidbody>().velocity = projectileSpeed * firePoint.forward;
        spawnedProjectile.GetComponent<BasicProjectile>().damage = pistolDamage;
        Destroy(spawnedProjectile, 5.0f); 
    }
}
