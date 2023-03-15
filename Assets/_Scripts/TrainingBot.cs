using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBot : MonoBehaviour
{
    [Header("Hero Bot")]
    [SerializeField] private HeroScriptableObject heroBot;

    //Hero Name
    public string heroName;

    //Bot Health Stats
    public bool hasHealth;
    public bool hasArmor;
    public bool hasShield;
    public bool hasOverHP;

    public int maxHealth;
    public int maxArmor;
    public int maxShield;
    public int maxOverHP;

    public int totalLife;

    private float movementSpeed;

    private void Awake()
    {
        SetBotStats();
    }

    private void SetBotStats()
    {
        heroName = heroBot.heroName;

        hasHealth = heroBot.hasHealth;
        hasArmor = heroBot.hasArmor;
        hasShield = heroBot.hasShield;
        hasOverHP = heroBot.hasOverHP;

        maxHealth = heroBot.health;
        maxArmor = heroBot.armor;
        maxShield = heroBot.shield;
        maxOverHP = heroBot.overHP;

        totalLife = maxHealth + maxArmor + maxShield + maxOverHP;

        movementSpeed = heroBot.moveSpeed;
    }
}
