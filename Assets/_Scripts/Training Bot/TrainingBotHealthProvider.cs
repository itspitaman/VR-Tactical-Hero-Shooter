using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBotHealthProvider : MonoBehaviour
{
    [Header("Training Bot")]
    [SerializeField] private TrainingBot bot;

    [Header("Healthbar")]
    [SerializeField] private HealthbarConstructor healthbar;

    private bool hasHealth;
    private bool hasArmor;
    private bool hasShield;
    private bool hasOverHP;

    private int maxHealth;
    private int maxArmor;
    private int maxShield;
    private int maxOverHP;

    private int maxTotalLife;

    [Header("Bot Health Values")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentArmor;
    [SerializeField] private float currentShield;
    [SerializeField] private float currentOverHP;

    [SerializeField] private float currentTotalLife;

    //Special Values
    private float armorDamageReduction = 0.30f;

    [Header("Debug")]
    [SerializeField] private bool takeDamage;
    [SerializeField] private bool takeHeal;
    [SerializeField] private int damage;
    [SerializeField] private int heal;

    private void Start()
    {
        GetBotStats();
        SetCurrentStats();
        CreateHealthbar();
    }

    private void Update()
    {
        if (takeDamage) TakeDamage(damage); takeDamage = false;
        if (takeHeal) Heal(heal); takeHeal = false;

        if (currentHealth <= 0)
            StartCoroutine(Revive());
    }

    #region SetUp
    private void GetBotStats()
    {
        hasHealth = bot.hasHealth;
        hasArmor = bot.hasArmor;
        hasShield = bot.hasShield;
        hasOverHP = bot.hasOverHP;

        maxHealth = bot.maxHealth;
        maxArmor = bot.maxArmor;
        maxShield = bot.maxShield;
        maxOverHP = bot.maxOverHP;

        maxTotalLife = bot.totalLife;
    }

    private void SetCurrentStats()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        currentShield = maxShield;
        currentOverHP = maxOverHP;

        currentTotalLife = maxTotalLife;
    }

    private void CreateHealthbar()
    {
        healthbar.CreatePlayerHealthbar(hasHealth, hasArmor, hasShield, hasOverHP,
                                        maxHealth, maxArmor, maxShield, maxOverHP,
                                        bot.totalLife);
    }
    #endregion

    public void TakeDamage(float damage)
    {
        if (hasOverHP)
        {

        }

        if (hasShield && (damage > 0))
        {
            if (damage > currentShield)
            {
                damage -= currentShield;
                currentShield = 0;
            }
            else
            {
                currentShield -= damage;
                damage = 0;
            }

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }

        if (hasArmor && (damage > 0))
        {
            float damageReduced = damage * armorDamageReduction;
            damage -= Mathf.RoundToInt(damageReduced);

            if (damage > currentArmor)
            {
                damage -= currentArmor;
                currentArmor = 0;
            }
            else
            {
                currentArmor -= damage;
                damage = 0;
            }

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }

        if (hasHealth && (damage > 0))
        {
            currentHealth -= damage;
            if (currentHealth <= 0) currentHealth = 0;

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }
    }

    public void Heal(float heal)
    {
        if (hasHealth && (heal > 0) && (currentHealth < maxHealth))
        {
            if ((heal + currentHealth) > maxHealth)
            {
                heal = (currentHealth + heal) - maxHealth;
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += heal;
                heal = 0;
            }

            if (currentHealth > maxHealth) currentHealth = maxHealth;

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }

        if (hasArmor && (heal > 0))
        {
            if ((heal + currentArmor) > maxArmor)
            {
                heal = (currentArmor + heal) - maxArmor;
                currentArmor = maxArmor;
            }
            else
            {
                currentArmor += heal;
                heal = 0;
            }

            if (currentArmor > maxArmor) currentArmor = maxArmor;

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }

        if (hasShield && (heal > 0))
        {
            if ((heal + currentShield) > maxShield)
            {
                heal = maxHealth - currentHealth;
                currentShield = maxShield;
            }
            else
            {
                currentShield += heal;
                heal = 0;
            }

            if (currentShield > maxShield) currentShield = maxShield;

            currentTotalLife = currentHealth + currentArmor + currentShield + currentOverHP;
            healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
        }

        if (hasOverHP)
        {

        }
    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(2f);
        SetCurrentStats();
        healthbar.UpdateHealthbar(currentTotalLife, maxTotalLife);
    }

    private void RegenerateShield()
    {

    }
}
