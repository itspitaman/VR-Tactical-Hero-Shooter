using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarConstructor : MonoBehaviour
{
    [Header("Fill References")]
    [SerializeField] private GameObject healthFillPanel;
    [SerializeField] private float fillSpeed;

    //References to fills (might have multiple fills for each type of life)
    private Image healthFill;

    [Header("Prefab References")]
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject armorPanel;
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private GameObject overHPPanel;
    [SerializeField] private GameObject maskPanel;

    //Refrences to the blocks
    private HealthBlocks healthBlocks;
    private ArmorBlocks armorBlocks;
    private ShieldBlocks shieldBlocks;
    private OverBlocks overBlocks;
    private MaskBlocks maskBlocks;

    [Header("Health Types")]
    [SerializeField] private bool hasHealth;
    [SerializeField] private bool hasArmor;
    [SerializeField] private bool hasShield;
    [SerializeField] private bool hasOverHP;

    [Header("Health Values")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxArmor;
    [SerializeField] private int maxShield;
    [SerializeField] private int maxOverHP;

    private int currentMaxHealth;
    private int currentMaxArmor;
    private int currentMaxShield;
    private int currentMaxOverHP;

    private int maxPlayerHealth;
    private int currentMaxPlayerHealth;

    private void Awake()
    {
        healthFill = healthFillPanel.GetComponent<Image>();

        healthBlocks = healthPanel.GetComponent<HealthBlocks>();
        armorBlocks = armorPanel.GetComponent<ArmorBlocks>();
        shieldBlocks = shieldPanel.GetComponent<ShieldBlocks>();
        overBlocks = overHPPanel.GetComponent<OverBlocks>();

        currentMaxHealth = maxHealth;
        currentMaxArmor = maxArmor;
        currentMaxShield = maxShield;
        currentMaxOverHP = maxOverHP;

        maskBlocks = maskPanel.GetComponent<MaskBlocks>();

        maxPlayerHealth = currentMaxHealth + currentMaxArmor + currentMaxShield + currentMaxOverHP;
        currentMaxPlayerHealth = maxPlayerHealth;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void CreateHealthbar()
    {
        if (hasHealth)
        {
            healthPanel.SetActive(true);
            healthBlocks.SetHealthBlocks(maxHealth);
        }
        else
            healthPanel.SetActive(false);

        if (hasArmor)
        {
            armorPanel.SetActive(true);
            armorBlocks.SetArmorBlocks(maxArmor);
        }
        else
            armorPanel.SetActive(false);

        if (hasShield)
        {
            shieldPanel.SetActive(true);
            shieldBlocks.SetShieldBlocks(maxShield);
        }
        else
            shieldPanel.SetActive(false);

        if (hasOverHP)
        {
            overHPPanel.SetActive(true);
            overBlocks.SetOverBlocks(maxOverHP);
        }
        else
            overHPPanel.SetActive(false);
    }

    private void CreateMask()
    {
        maskBlocks.SetMaskBlocks(maxPlayerHealth);
    }

    #region Methods for Update() and testing
    private void ActivatePanels()
    {
        if (hasHealth) healthPanel.SetActive(true);
        else healthPanel.SetActive(false);

        if (hasArmor) armorPanel.SetActive(true);
        else armorPanel.SetActive(false);

        if (hasShield) shieldPanel.SetActive(true);
        else shieldPanel.SetActive(false);

        if (hasOverHP) overHPPanel.SetActive(true);
        else overHPPanel.SetActive(false);
    }

    private void UpdateAllBars()
    {
        if (hasHealth && (maxHealth != currentMaxHealth))
        {
            currentMaxHealth = maxHealth;
            healthBlocks.UpdateHealthBlocks(maxHealth);
        }

        if (hasArmor && (maxArmor != currentMaxArmor))
        {
            currentMaxArmor = maxArmor;
            armorBlocks.UpdateArmorBlocks(maxArmor);
        }

        if (hasShield && (maxShield != currentMaxShield))
        {
            currentMaxShield = maxShield;
            shieldBlocks.UpdateShieldBlocks(maxShield);
        }

        if ( hasOverHP && (maxOverHP != currentMaxOverHP))
        {
            currentMaxOverHP = maxOverHP;
            overBlocks.UpdateOverBlocks(maxOverHP);
        }
    }

    private void UpdateMask()
    {
        if (!hasHealth) currentMaxHealth = 0;
        if (!hasArmor) currentMaxArmor = 0;
        if (!hasShield) currentMaxShield = 0;
        if (!hasOverHP) currentMaxOverHP = 0;

        maxPlayerHealth = currentMaxHealth + currentMaxArmor + currentMaxShield + currentMaxOverHP;

        if (currentMaxPlayerHealth != maxPlayerHealth)
        {
            currentMaxPlayerHealth = maxPlayerHealth;
            maskBlocks.UpdateMaskBlocks(maxPlayerHealth);
        }
    }
    #endregion

    #region Healthbar Creation
    public void CreatePlayerHealthbar(bool hasHealth, bool hasArmor, bool hasShield, bool hasOverHP, int health, int armor, int shield, int overHP, int totalHealth)
    {
        if (hasHealth)
        {
            healthPanel.SetActive(true);
            Debug.Log(healthBlocks);
            healthBlocks.SetHealthBlocks(health);
        }
        else
            healthPanel.SetActive(false);

        if (hasArmor)
        {
            armorPanel.SetActive(true);
            armorBlocks.SetArmorBlocks(armor);
        }
        else
            armorPanel.SetActive(false);

        if (hasShield)
        {
            shieldPanel.SetActive(true);
            shieldBlocks.SetShieldBlocks(shield);
        }
        else
            shieldPanel.SetActive(false);

        if (hasOverHP)
        {
            overHPPanel.SetActive(true);
            overBlocks.SetOverBlocks(overHP);
        }
        else
            overHPPanel.SetActive(false);

        CreatePlayerMask(totalHealth);
        SetFillAmounts();
    }

    private void CreatePlayerMask(int totalLife)
    {
        maskBlocks.SetMaskBlocks(totalLife);
    }

    private void SetFillAmounts()
    {
        healthFill.fillAmount = 1;
    }
    #endregion

    public void UpdateHealthbar(float currentTotalLife, float maxTotalLife)
    {
        float currentFillPercentage = currentTotalLife / maxTotalLife;
        healthFill.fillAmount = currentFillPercentage;
    }
}
