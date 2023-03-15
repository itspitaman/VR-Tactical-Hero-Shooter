using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlocks : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private List<GameObject> currentShield = new List<GameObject>();

    public void SetShieldBlocks(int maxShield)
    {
        int blocksToCreate = maxShield / 25;

        for (int block = 0; block < blocksToCreate; block++)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation, transform);
            currentShield.Add(newBlock);
        }
    }

    public void UpdateShieldBlocks(int newMaxShield)
    {
        foreach (GameObject block in currentShield)
            Destroy(block);

        currentShield.Clear();
        SetShieldBlocks(newMaxShield);
    }
}
