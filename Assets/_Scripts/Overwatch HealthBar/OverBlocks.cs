using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverBlocks : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private List<GameObject> currentOverHP = new List<GameObject>();

    public void SetOverBlocks(int maxOverHP)
    {
        int blocksToCreate = maxOverHP / 25;

        for (int block = 0; block < blocksToCreate; block++)
        {
            GameObject newBlock = Instantiate(blockPrefab, transform.position, transform.rotation, transform);
            currentOverHP.Add(newBlock);
        }
    }

    public void UpdateOverBlocks(int newMaxOverHP)
    {
        foreach (GameObject block in currentOverHP)
            Destroy(block);

        currentOverHP.Clear();
        SetOverBlocks(newMaxOverHP);
    }
}
