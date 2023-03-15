using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagepack : MonoBehaviour
{
    [SerializeField] private GameObject icon;

    [SerializeField] private int damageAmount;
    [SerializeField] private float cooldown;
    private bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player Health"))
        {
            other.GetComponent<PlayerHealthProvider>().TakeDamage(damageAmount);
            StartCoroutine(Cooldown());
        }

        IEnumerator Cooldown()
        {
            isActive = false;
            icon.SetActive(false);

            yield return new WaitForSeconds(cooldown);

            icon.SetActive(true);
            isActive = true;
        }
    }
}
