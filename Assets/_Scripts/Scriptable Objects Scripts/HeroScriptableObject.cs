using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Hero")]

public class HeroScriptableObject : ScriptableObject
{
    [Header("Identity")]
    public string heroName;

    [Header("Stats")]
    public bool hasHealth;
    public bool hasArmor;
    public bool hasShield;
    public bool hasOverHP;

    public int health;
    public int armor;
    public int shield;
    public int overHP;

    [Header("Movement")]
    public float moveSpeed;
}
