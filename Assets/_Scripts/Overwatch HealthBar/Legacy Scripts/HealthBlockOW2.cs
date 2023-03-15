using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HealthBlockOW2 : MonoBehaviour
{
    [SerializeField] private GameObject healthBlockPrefab;

    [SerializeField] private List<GameObject> currentHealth = new List<GameObject>();
    private List<Image> currentBlockFill = new List<Image>();

    public void SetHealthbar(int maxHP)
    {
        int healthBlocksToCreate = maxHP / 25;

        for (int block = 0 ; block < healthBlocksToCreate ; block++)
        {
            GameObject newBlock = Instantiate(healthBlockPrefab, transform.position, transform.rotation, transform);
            currentHealth.Add(newBlock);
        }
    }

    public void UpdateHealthBar(int newMaxHP)
    {
        foreach (GameObject block in currentHealth)
        {
            Destroy(block);
        }

        SetHealthbar(newMaxHP);
    }

    public void TakeDamage(int damageTaken)
    {
        float damageToTake = (float)damageTaken / 25;
        damageToTake = (float)Math.Round(damageToTake, 1);
        Debug.Log("Initial damage taken: " + damageToTake);

        for (int block = currentHealth.Count - 1 ; block >= 0 ; block--)   //Iterates between all the health blocks
        {
            Debug.Log(currentHealth[0]);
            //if (currentHealth[0].GetComponent<FindFillComponent>().fill.fillAmount <= 0)
            //{
            //    Debug.Log("You are dead already lol");
            //    break;
            //}

            float currentBlockHealth = currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount;

            if (currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount > 0 && damageToTake <= 1)   //Checks if we have health and the damage is less than 1
            {
                if (currentBlockHealth < damageToTake)
                {
                    Debug.Log("current health " + currentBlockHealth);

                    damageToTake = Mathf.Abs(currentBlockHealth - damageToTake);

                    Debug.Log("damage to take (abs) " + damageToTake);

                    currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount = 0;
                    if(currentHealth[block - 1] != null)
                        currentHealth[block - 1].GetComponent<FindFillComponent>().fill.fillAmount -= damageToTake;
                    currentBlockHealth = currentHealth[block - 1].GetComponent<FindFillComponent>().fill.fillAmount;

                    Debug.Log("damage taken " + damageToTake);
                    Debug.Log("remainging health " + currentBlockHealth);
                    break;
                }

                Debug.Log("current health " + currentBlockHealth);
                Debug.Log("damage to take " + damageToTake);
                currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount -= damageToTake;
                Debug.Log("damage taken " + damageToTake);
                currentBlockHealth = currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount;
                Debug.Log("remaining health " + currentBlockHealth);
                break;
            }
            else if (currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount > 0 && damageToTake >= 1)   //Check is we have health and the damage is greater than 1
            {
                currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount = 0;
                damageToTake -= 1;
                Debug.Log("damage was greater than 1, new damage to take " + damageToTake);
            }
        }
    }

    public void RecieveHeal(int healingRecieved)
    {
        if (currentHealth[currentHealth.Count - 1].GetComponent<FindFillComponent>().fill.fillAmount >= 1)
            return;

        float healingToRecieve = (float)healingRecieved / 25;

        for (int block = 0 ; block <= currentHealth.Count - 1 ; block++)
        {
            float currentBlockHealth = currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount;                      

            if (currentBlockHealth < 1) //Can be healed
            {
                Debug.Log(currentHealth[block]);

                if (healingToRecieve >= 1) //Healing amount is greater than 25 (a block)
                {
                    Debug.Log("healing is greater than 1");
                    currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount = 1; //Set this block's fill amount to 25
                    healingToRecieve -= 1;
                }
                else if (healingToRecieve < 1) //Healing amount is less 25 (a block)
                {
                    Debug.Log("healing is less than 1");

                    if (currentBlockHealth + healingToRecieve > 1)
                    {
                        Debug.Log("healing would fully heal this block");

                        healingToRecieve = Mathf.Abs(currentBlockHealth - healingToRecieve);
                        currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount = 1;
                        currentHealth[block + 1].GetComponent<FindFillComponent>().fill.fillAmount = healingToRecieve;
                        break;
                    }

                    currentHealth[block].GetComponent<FindFillComponent>().fill.fillAmount += healingToRecieve;
                    break;
                }
            }

            Debug.Log(currentHealth[block] + "cannot be healed");
        }
    }
}
