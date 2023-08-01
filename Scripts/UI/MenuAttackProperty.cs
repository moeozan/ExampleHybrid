using System.Collections;
using UnityEngine;

public class MenuAttackProperty: MenuProperty
{
    [SerializeField] private AttackProperties m_Property;

    public override void Awake()
    {
        StartCoroutine(Wake());
    }

    public IEnumerator Wake()
    {
        yield return new WaitUntil(() => DatabaseManager.instance.Data != null);
        m_Talents = DatabaseManager.instance.GetTalents();
        increaseValue = IncreaseValueReturner(m_Property);
        switch (m_Property)
        {
            case AttackProperties.Damage:
                startValue = m_Talents.Attack.Damage;
                IncreaseCost = m_Talents.Attack.DamageCost;
                increaseProperty.onClick.AddListener(() => NewDamageParameters(IncreaseCost));
                break;
            case AttackProperties.Speed:
                startValue = m_Talents.Attack.Speed;
                IncreaseCost = m_Talents.Attack.SpeedCost;
                increaseProperty.onClick.AddListener(() => NewSpeedParameters(IncreaseCost));
                break;
            case AttackProperties.Range:
                startValue = m_Talents.Attack.Range;
                IncreaseCost = m_Talents.Attack.RangeCost;
                increaseProperty.onClick.AddListener(() => NewRangeParameters(IncreaseCost));
                break;
            case AttackProperties.DamagePerMeter:
                startValue = m_Talents.Attack.DamagePerMeter;
                IncreaseCost = m_Talents.Attack.DamagePerMeterCost;
                increaseProperty.onClick.AddListener(() => NewDamagePerMeterParameters(IncreaseCost));
                break;
            case AttackProperties.Critical:
                startValue = m_Talents.Attack.Critical;
                IncreaseCost = m_Talents.Attack.CriticalCost;
                increaseProperty.onClick.AddListener(() => NewCriticalParameters(IncreaseCost));
                break;
            case AttackProperties.Multiplier:
                startValue = m_Talents.Attack.Multiplier;
                IncreaseCost = m_Talents.Attack.MultiplierCost;
                increaseProperty.onClick.AddListener(() => NewMultiplierParameters(IncreaseCost));
                break;
        }
        increaseProperty.onClick.AddListener(IncreaseCostHandler);
        propertyName.text = m_Property.ToString();
        base.Awake();
    }
    private void IncreaseCostHandler()
    {
        IncreaseCost *= 1.1f;
        IncreaseCost = GameUtilities.FloatHandler(IncreaseCost);
    }
    private float IncreaseValueReturner(AttackProperties property)
    {
        AttackPropertyIncreaseValue.TryGetValue(property, out float value);
        return GameUtilities.FloatHandler(value);
    }
    private void NewDamageParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.Damage = GameUtilities.FloatHandler(Value);
        data.Attack.DamageCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }

    private void NewSpeedParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.Speed = GameUtilities.FloatHandler(Value);
        data.Attack.SpeedCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }

    private void NewRangeParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.Range = GameUtilities.FloatHandler(Value);
        data.Attack.RangeCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }

    private void NewDamagePerMeterParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.DamagePerMeter = GameUtilities.FloatHandler(Value);
        data.Attack.DamagePerMeterCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }


    private void NewCriticalParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.Critical = GameUtilities.FloatHandler(Value);
        data.Attack.CriticalCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }


    private void NewMultiplierParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack.Multiplier = GameUtilities.FloatHandler(Value);
        data.Attack.MultiplierCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Attack);
    }

    private void SetTalents(AttackTalents attackTalent)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Attack = attackTalent;
        DatabaseManager.instance.AddGold(-IncreaseCost);
        DatabaseManager.instance.UpdateTalents(data);
    }
}
