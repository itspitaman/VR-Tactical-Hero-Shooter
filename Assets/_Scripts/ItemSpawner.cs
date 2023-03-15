using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemSpawnPoint;
    private bool spawned;

    private void Start()
    {
        GameObject spawnedItem = Instantiate(itemPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
    }

}
