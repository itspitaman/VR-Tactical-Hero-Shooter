using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVR : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private HeroScriptableObject hero;

    //Hero Health Stats
    public bool hasHealth;
    public bool hasArmor;
    public bool hasShield;
    public bool hasOverHP;

    public int maxHealth;
    public int maxArmor;
    public int maxShield;
    public int maxOverHP;

    public int totalLife;

    //Hero Movement Stats
    private float movementSpeed;

    private void Awake()
    {
        SetHeroStats();
    }

    private void SetHeroStats()
    {
        hasHealth = hero.hasHealth;
        hasArmor = hero.hasArmor;
        hasShield = hero.hasShield;
        hasOverHP = hero.hasOverHP;

        maxHealth = hero.health;
        maxArmor = hero.armor;
        maxShield = hero.shield;
        maxOverHP = hero.overHP;

        totalLife = maxHealth + maxArmor + maxShield + maxOverHP;

        movementSpeed = hero.moveSpeed;
    }
}
