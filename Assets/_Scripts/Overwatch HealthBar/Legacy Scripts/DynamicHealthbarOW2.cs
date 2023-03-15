using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicHealthbarOW2 : MonoBehaviour
{
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject armorPanel;
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private GameObject overhealthPanel;

    private HealthBlockOW2 healthBlocks;
    private ArmorBlockOW2 armorBlocks;
    private ShieldBlockOW2 shieldBlocks;
    private OverHealthOW2 overHealthBlocks;

    [SerializeField] private bool hasHealth;
    [SerializeField] private bool hasArmor;
    [SerializeField] private bool hasShield;
    [SerializeField] private bool hasOverhealth;

    [SerializeField] private int maxHealth;
    [SerializeField] private int maxArmor;
    [SerializeField] private int maxShield;
    [SerializeField] private int maxOverHealth;

    private int currentMaxHealth;
    private int currentMaxArmor;
    private int currentMaxShield;
    private int currentMaxOverHealth;

    //test variables
    [SerializeField] private bool takeDamageNow;
    [SerializeField] private int damageToTake;
    [SerializeField] private bool recieveHealNow;
    [SerializeField] private int healToRecieve;

    private void Start()
    {
        healthBlocks = healthPanel.GetComponent<HealthBlockOW2>();
        armorBlocks = armorPanel.GetComponent<ArmorBlockOW2>();
        shieldBlocks = shieldPanel.GetComponent<ShieldBlockOW2>();
        overHealthBlocks = overhealthPanel.GetComponent<OverHealthOW2>();

        currentMaxHealth = maxHealth;
        currentMaxArmor = maxArmor;
        currentMaxShield = maxShield;
        currentMaxOverHealth = maxOverHealth;

        CreateHealthbar();
    }

    private void Update()
    {
        ActivatePanels();
        UpdateAllBars();

        if (takeDamageNow)
        {
            TakeSomeDamage(damageToTake);
            takeDamageNow = false;
        }

        if (recieveHealNow)
        {
            GetSomeHeals(healToRecieve);
            recieveHealNow = false;
        }
    }

    //----------------------------------------------------------------------------------

    void CreateHealthbar()
    {
        if (hasHealth)
        {
            healthPanel.SetActive(true);
            healthBlocks.SetHealthbar(maxHealth);
        }
        else
            healthPanel.SetActive(false);

        if (hasArmor)
        {
            armorPanel.SetActive(true);
            armorBlocks.SetArmorBar(maxArmor);
        }
        else
            armorPanel.SetActive(false);

        if (hasShield)
        {
            shieldPanel.SetActive(true);
            shieldBlocks.SetShieldBar(maxShield);
        }
        else
            shieldPanel.SetActive(false);

        if (hasOverhealth)
        {
            overhealthPanel.SetActive(true);
            overHealthBlocks.SetOverHealthBar(maxOverHealth);
        }
        else
            overhealthPanel.SetActive(false);
    }

    void ActivatePanels()
    {
        if (hasHealth) healthPanel.SetActive(true);
        else healthPanel.SetActive(false);

        if (hasArmor) armorPanel.SetActive(true);
        else armorPanel.SetActive(false);

        if (hasShield) shieldPanel.SetActive(true);
        else shieldPanel.SetActive(false);

        if (hasOverhealth) overhealthPanel.SetActive(true);
        else overhealthPanel.SetActive(false);
    }

    void UpdateAllBars()
    {
        if (maxHealth != currentMaxHealth)
        {
            currentMaxHealth = maxHealth;
            healthBlocks.UpdateHealthBar(maxHealth);
        }

        if (maxArmor != currentMaxArmor)
        {
            currentMaxArmor = maxArmor;
            armorBlocks.UpdateArmorBar(maxArmor);
        }

        if (maxShield != currentMaxShield)
        {
            currentMaxShield = maxShield;
            shieldBlocks.UpdateShieldBar(maxShield);
        }

        if (maxOverHealth != currentMaxOverHealth)
        {
            currentMaxOverHealth = maxOverHealth;
            overHealthBlocks.UpdateOverHealthBar(maxOverHealth);
        }
    }

    void TakeSomeDamage(int damageTaken)
    {
        if (hasOverhealth)
        {

        }

        if (hasShield)
        {

        }

        if (hasArmor)
        {

        }

        if (hasHealth)
        {
            healthBlocks.TakeDamage(damageTaken);
        }
    }

    void GetSomeHeals(int healTaken)
    {
        if (hasOverhealth)
        {
            //Overhealth cannot be healed
        }

        if (hasShield)
        {

        }

        if (hasArmor)
        {

        }

        if (hasHealth)
        {
            healthBlocks.RecieveHeal(healToRecieve);
        }
    }
}
