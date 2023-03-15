using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBlocks : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private List<GameObject> currentArmor = new List<GameObject>();

    public void SetArmorBlocks(int maxArmor)
    {
        int blocksToCreate = maxArmor / 25;

        for (int block = 0; block < blocksToCreate; block++)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation, transform);
            currentArmor.Add(newBlock);
        }
    }

    public void UpdateArmorBlocks(int newMaxArmor)
    {
        foreach (GameObject block in currentArmor)
            Destroy(block);

        currentArmor.Clear();
        SetArmorBlocks(newMaxArmor);
    }
}
