using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBlocks : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private List<GameObject> currentHealth = new List<GameObject>();

    public void SetHealthBlocks(int maxHP)
    {
        int blocksToCreate = maxHP / 25;

        for (int block = 0 ; block < blocksToCreate ; block++)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation, transform);
            currentHealth.Add(newBlock);
        }
    }

    public void UpdateHealthBlocks(int newMaxHP)
    {
        foreach (GameObject block in currentHealth)
            Destroy(block);

        currentHealth.Clear();
        SetHealthBlocks(newMaxHP);
    }
}
