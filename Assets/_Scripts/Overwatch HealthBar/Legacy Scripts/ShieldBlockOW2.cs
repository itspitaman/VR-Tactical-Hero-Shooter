using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockOW2 : MonoBehaviour
{
    [SerializeField] private GameObject shieldBlockPrefab;
    private List<GameObject> currentShield = new List<GameObject>();

    private void Update()
    {

    }

    public void SetShieldBar(int maxShield)
    {
        int shieldBlocksToCreate = maxShield / 25;

        for (int blocks = 0; blocks < shieldBlocksToCreate; blocks++)
        {
            currentShield.Add(Instantiate(shieldBlockPrefab, transform.position, transform.rotation, transform)); //gameobject = instantiate
        }
    }

    public void UpdateShieldBar(int newMaxShield)
    {
        foreach (GameObject block in currentShield)
        {
            Destroy(block);
        }

        SetShieldBar(newMaxShield);
    }
}
