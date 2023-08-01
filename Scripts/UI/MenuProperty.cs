using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuProperty: MonoBehaviour
{
    protected Talents m_Talents;
    protected readonly Dictionary<AttackProperties, float> AttackPropertyIncreaseValue = new()
    {
        {AttackProperties.Damage, 5f},
        {AttackProperties.Speed, .11f},
        {AttackProperties.Range, .1f},
        {AttackProperties.DamagePerMeter, 1f},
        {AttackProperties.Critical, 1f},
        {AttackProperties.Multiplier, .1f},
    };

    protected readonly Dictionary<DefenseProperties, float> DefensePropertyIncreaseValue = new()
    {
        {DefenseProperties.Health, 5f},
        {DefenseProperties.HealthRegen, .13f},
        {DefenseProperties.Mana, 5f},
        {DefenseProperties.ManaRegen, .13f},
    };

    protected readonly Dictionary<UtilityProperties, float> UtilityPropertyIncreaseValue = new()
    {
        {UtilityProperties.GoldPerEnemy, .1f},
        {UtilityProperties.ExpPerEnemy, .1f},
        {UtilityProperties.GoldPerLevel, 3f},
        {UtilityProperties.ExpPerLevel, 3f},
    };

    [SerializeField] protected TextMeshProUGUI propertyName;
    [SerializeField] protected TextMeshProUGUI propertyValue;
    [SerializeField] protected Button increaseProperty;
    [SerializeField] private TextMeshProUGUI costText;

    protected float startValue;
    protected float increaseValue;
    public float Value { get; private set; }
    public float IncreaseCost { get; protected set; }

    public virtual void Awake()
    {
        Value = GameUtilities.FloatHandler(startValue);
        propertyValue.text = Value.ToString("F1");
        costText.text = IncreaseCost.ToString("F1");
        CheckCost();
    }

    protected virtual void IncreaseValue()
    {
        Value += increaseValue;
        Value = GameUtilities.FloatHandler(Value);
        propertyValue.text = Value.ToString();
        IncreaseCost = GameUtilities.FloatHandler(IncreaseCost);
        costText.text = IncreaseCost.ToString();
    }

    public virtual void Start()
    {
        increaseProperty.onClick.AddListener(IncreaseValue);
        increaseProperty.onClick.AddListener(CheckCost);
    }

    private void CheckCost()
    {
        if (DatabaseManager.instance.GetGold() < IncreaseCost)
            increaseProperty.interactable = false;

        else
            increaseProperty.interactable = true;
    }

    protected void OnDestroy()
    {
        increaseProperty.onClick.RemoveAllListeners();
    }
}
