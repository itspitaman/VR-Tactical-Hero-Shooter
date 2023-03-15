using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour
{
    [SerializeField] private GameObject icon;

    [SerializeField] private int healAmount;
    [SerializeField] private float cooldown;
    private bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the healthpack");

        if (isActive && other.CompareTag("Player Health"))
        {
            other.GetComponent<PlayerHealthProvider>().Heal(healAmount);
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
