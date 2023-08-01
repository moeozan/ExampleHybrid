using System;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class PropertyManager : MonoBehaviour
{
    [Header("Attack Properties")]
    [SerializeField] private AttackProperty damageProperty;
    [SerializeField] private AttackProperty speedProperty;
    [SerializeField] private AttackProperty rangeProperty;
    [SerializeField] private AttackProperty damagePerRangeProperty;
    [SerializeField] private AttackProperty criticalProperty;
    [SerializeField] private AttackProperty multiplierProperty;

    [Header("Defense Properties")]
    [SerializeField] private DefenseProperty healthProperty;
    [SerializeField] private DefenseProperty healthRegenProperty;
    [SerializeField] private DefenseProperty manaProperty;
    [SerializeField] private DefenseProperty manaRegenProperty;

    [Header("Utility Properties")]
    [SerializeField] private UtilityProperty goldPerEnemyProperty;
    [SerializeField] private UtilityProperty expPerEnemyProperty;
    [SerializeField] private UtilityProperty goldPerLevelProperty;
    [SerializeField] private UtilityProperty expPerLevelProperty;

    [Header("User Cost Panel")]
    [SerializeField] private UserCosts userCosts;
    [Header("Player UI")]
    [SerializeField] private PlayerUI playerUI;
    [Header("Game Over Canvas")]
    [SerializeField] private Canvas gameOverCanvas;

    public float Damage { get; private set; }
    public float AttackSpeed { get; private set; }
    public float Range { get; private set; }
    public float DamagePerRange { get; private set; }
    public float Critical { get; private set; }
    public float Multiplier { get; private set; }

    public float Health { get; private set; }
    public float HealthRegen { get; private set; }
    public float Mana { get; private set; }
    public float ManaRegen { get; private set; }

    public float ExperiencePerEnemy { get; private set; }
    public float ExperincePerLevel { get; private set; }
    public float GoldPerEnemy { get; private set; }
    public float GoldPerLevel { get; private set; }

    public float TotalExpAmount { get; private set; }
    public float TotalGoldAmount { get; private set; }
    
    public float CurrentHealth { get; private set; }
    public float CurrentMana { get; private set; }
    public bool IsDamageCritical { get; private set; }

    public static Action<float> DamageHandle;
    public static Action<float> SpeedHandle;
    public static Action<float> RangeHandle;
    public static Action<float> DamagePerRangeHandle;
    public static Action<float> CriticalHandle;
    public static Action<float> MultiplierHandle;

    public static Action<float> HealthHandle;
    public static Action<float> HealthRegenHandle;
    public static Action<float> ManaHandle;
    public static Action<float> ManaRegenHandle;

    public static Action<float> ExpPerEnemyHandle;
    public static Action<float> ExpPerLevelHandle;
    public static Action<float> GoldPerEnemyHandle;
    public static Action<float> GoldPerLevelHandle;

    public static Action<float> CurrentHealthHandle;
    public static Action<float> ExpHandle;
    public static Action<float> GoldHandle;

    public static PropertyManager instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        ExpHandle += ExpHandler;
        GoldHandle += GoldHandler;

        RangeHandle += RangeHandler;
        HealthHandle += HealthHandler;
        HealthRegenHandle += HealthRegenHandler;
        ManaRegenHandle += ManaRegenHandler;
        ManaHandle += ManaHandler;
        SpeedHandle += SpeedHandler;
        DamageHandle += DamageHandler;
        DamagePerRangeHandle += DamagePerRangeHandler;
        CriticalHandle += CriticalHandler;
        MultiplierHandle += MultiplierHandler;
        ExpPerLevelHandle += ExperiencePerLevelHandler;
        GoldPerLevelHandle += GoldPerLevelHandler;
        ExpPerEnemyHandle += ExperiencePerEnemyHandler;
        GoldPerEnemyHandle += GoldPerEnemyHandler;
        CurrentHealthHandle += CurrentHealthHandler;

        TotalExpAmount = 0;
    }

    private void Start()
    {

        Damage = damageProperty.Value;
        AttackSpeed = speedProperty.Value;
        Range = rangeProperty.Value;
        DamagePerRange = damagePerRangeProperty.Value;
        Critical = criticalProperty.Value;
        Multiplier = multiplierProperty.Value;

        ExperiencePerEnemy = expPerEnemyProperty.Value;
        ExperincePerLevel = expPerLevelProperty.Value;
        GoldPerEnemy = goldPerEnemyProperty.Value;
        GoldPerLevel = goldPerLevelProperty.Value;

        Health = healthProperty.Value;
        HealthRegen = healthRegenProperty.Value;
        Mana = manaProperty.Value;
        ManaRegen = manaRegenProperty.Value;
        CurrentHealth = Health;
        CurrentMana = Mana;

        HealthHandler(0);
        ManaHandler(0);

        InvokeRepeating(nameof(HealthRegener), 1f, 1f);
    }

    private void OnDestroy()
    {
        ExpHandle -= ExpHandler;
        GoldHandle -= GoldHandler;

        RangeHandle -= RangeHandler;
        HealthHandle -= HealthHandler;
        HealthRegenHandle -= HealthRegenHandler;
        ManaHandle -= ManaHandler;
        ManaRegenHandle-= ManaRegenHandler;
        SpeedHandle -= SpeedHandler;
        DamageHandle -= DamageHandler;
        DamagePerRangeHandle -= DamagePerRangeHandler;
        CriticalHandle -= CriticalHandler;
        MultiplierHandle -= MultiplierHandler;
        ExpPerLevelHandle -= ExperiencePerLevelHandler;
        GoldPerLevelHandle -= GoldPerLevelHandler;
        ExpPerEnemyHandle -= ExperiencePerEnemyHandler;
        GoldPerEnemyHandle -= GoldPerEnemyHandler;
        CurrentHealthHandle -= CurrentHealthHandler;
    }

    public void DamageCalculator(Vector3 pos)
    {
        IsDamageCritical = false;
        Damage = damageProperty.Value;
        Damage = GameUtilities.FloatHandler(Damage);
        int critic = UnityEngine.Random.Range(0, 101);
        float diff = Vector3.Distance(pos, transform.position);
        float DPM = diff * DamagePerRange / 1000;
        Damage += DPM;
        if (critic <= criticalProperty.Value)
        {
            Damage *= multiplierProperty.Value;
            IsDamageCritical = true;
        }
    }

    private void ExpHandler(float amount)
    {
        TotalExpAmount += amount;
        TotalExpAmount = GameUtilities.FloatHandler(TotalExpAmount);
        userCosts.ExperienceHandler(TotalExpAmount);
    }
    private void GoldHandler(float amount)
    {
        TotalGoldAmount += amount;
        TotalGoldAmount = GameUtilities.FloatHandler(TotalGoldAmount);
        userCosts.GoldHandler(TotalGoldAmount);
    }

    private void DamageHandler(float amount)
    {
        Damage += amount;
        Damage = GameUtilities.FloatHandler(Damage);
    }
    private void SpeedHandler(float amount)
    {
        AttackSpeed += amount;
        AttackSpeed = GameUtilities.FloatHandler(AttackSpeed);
    }

    private void RangeHandler(float amount)
    {
        Camera.main.GetComponent<CamHandler>().HandleCam();
        Range += amount;
        Range = GameUtilities.FloatHandler(Range);
        EnemyDetector.RangeHandle.Invoke(Range);
    }

    private void DamagePerRangeHandler(float amount)
    {
        DamagePerRange += amount;
        DamagePerRange = GameUtilities.FloatHandler(DamagePerRange);
    }

    private void CriticalHandler(float amount)
    {
        Critical += amount;
        Critical = GameUtilities.FloatHandler(Critical);
    }

    private void MultiplierHandler(float amount)
    {
        Multiplier += amount;
        Multiplier = GameUtilities.FloatHandler(Multiplier);
    }


    private void HealthHandler(float amount)
    {
        Health += amount;
        Health = GameUtilities.FloatHandler(Health);
        CurrentHealth += amount;
        CurrentHealth = GameUtilities.FloatHandler(CurrentHealth);
        playerUI.SetHealthUI(CurrentHealth, Health);
    }

    private void CurrentHealthHandler(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = GameUtilities.FloatHandler(CurrentHealth);
        playerUI.SetHealthUI(CurrentHealth, Health);
    }

    private void HealthRegenHandler(float amount)
    {
        HealthRegen += amount;
        HealthRegen = GameUtilities.FloatHandler(HealthRegen);
    }

    private void ManaHandler(float amount)
    {
        Mana += amount;
        Mana = GameUtilities.FloatHandler(Mana);
        CurrentMana += amount;
        CurrentMana = GameUtilities.FloatHandler(CurrentMana);
        playerUI.SetManaUI(CurrentMana, Mana);
    }

    private void ManaRegenHandler(float amount)
    {
        ManaRegen += amount;
        ManaRegen = GameUtilities.FloatHandler(ManaRegen);
    }

    private void ExperiencePerLevelHandler(float amount)
    {
        ExperincePerLevel += amount;
        ExperincePerLevel = GameUtilities.FloatHandler(ExperincePerLevel);
    }

    private void GoldPerLevelHandler(float amount)
    {
        GoldPerLevel += amount;
        GoldPerLevel = GameUtilities.FloatHandler(GoldPerLevel);
    }

    private void ExperiencePerEnemyHandler(float amount)
    {
        ExperiencePerEnemy += amount;
        ExperiencePerEnemy = GameUtilities.FloatHandler(ExperiencePerEnemy);
    }

    private void GoldPerEnemyHandler(float amount)
    {
        GoldPerEnemy += amount;
        GoldPerEnemy = GameUtilities.FloatHandler(GoldPerEnemy);
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Time.timeScale = 0f;
            gameOverCanvas.gameObject.SetActive(true);
            enabled = false;
        }
        else if (CurrentHealth >= Health) 
        {
            CurrentHealth = Health;
        }
    }

    private void HealthRegener()
    {
        if (CurrentHealth < 0)
        {
            return;
        }
        else if (CurrentHealth > 0 && CurrentHealth < Health)
        {
            CurrentHealth += HealthRegen;
            if (CurrentHealth >= Health)
            {
                CurrentHealth = Health;
            }
            playerUI.SetHealthUI(CurrentHealth, Health);
        }
    }
#if UNITY_EDITOR
    [Button("Give me 500 EX")]
    private void GiveMeExperience()
    {
        ExpHandler(500);
    }

#endif
}
