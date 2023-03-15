using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBlockOW2 : MonoBehaviour
{
    [SerializeField] private GameObject armorBlockPrefab;
    private List<GameObject> currentArmor = new List<GameObject>();

    private void Update()
    {

    }

    public void SetArmorBar(int maxArmor)
    {
        int armorBlocksToCreate = maxArmor / 25;

        for (int blocks = 0; blocks < armorBlocksToCreate; blocks++)
        {
            currentArmor.Add(Instantiate(armorBlockPrefab, transform.position, transform.rotation, transform)); //gameobject = instantiate
        }
    }

    public void UpdateArmorBar(int newMaxArmor)
    {
        foreach (GameObject block in currentArmor)
        {
            Destroy(block);
        }

        SetArmorBar(newMaxArmor);
    }
}
