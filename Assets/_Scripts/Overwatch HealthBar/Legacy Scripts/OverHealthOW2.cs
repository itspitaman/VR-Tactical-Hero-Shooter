using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHealthOW2 : MonoBehaviour
{
    [SerializeField] private GameObject overHealthBlockPrefab;
    private List<GameObject> currentOverHealth = new List<GameObject>();

    private void Update()
    {

    }

    public void SetOverHealthBar(int maxOverHP)
    {
        int overHealthBlocksToCreate = maxOverHP / 25;

        for (int blocks = 0; blocks < overHealthBlocksToCreate; blocks++)
        {
            currentOverHealth.Add(Instantiate(overHealthBlockPrefab, transform.position, transform.rotation, transform)); //gameobject = instantiate
        }
    }

    public void UpdateOverHealthBar(int newMaxOverHP)
    {
        foreach (GameObject block in currentOverHealth)
        {
            Destroy(block);
        }

        SetOverHealthBar(newMaxOverHP);
    }
}
