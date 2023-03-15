using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("We hit something: " + other.tag);

        if (other.CompareTag("Training Bot"))
        {
            other.GetComponent<TrainingBotHealthProvider>().TakeDamage(damage);
            Destroy(this);
        }
    }
}
