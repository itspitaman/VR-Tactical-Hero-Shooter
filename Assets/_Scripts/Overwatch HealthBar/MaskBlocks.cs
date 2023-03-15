using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBlocks : MonoBehaviour
{
    [SerializeField] private GameObject blockMaskPrefab;

    [SerializeField] private List<GameObject> currentMasks = new List<GameObject>();

    public void SetMaskBlocks(int maxHP)
    {
        int masksToCreate = maxHP / 25;

        for (int block = 0; block < masksToCreate; block++)
        {
            GameObject newBlock = Instantiate(blockMaskPrefab, transform.position, transform.rotation, transform);
            currentMasks.Add(newBlock);
        }
    }

    public void UpdateMaskBlocks(int newMaxHP)
    {
        foreach (GameObject block in currentMasks)
            Destroy(block);

        currentMasks.Clear();
        SetMaskBlocks(newMaxHP);
    }
}
