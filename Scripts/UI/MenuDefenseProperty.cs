using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDefenseProperty : MenuProperty
{
    [SerializeField] private DefenseProperties m_Property;

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
            case DefenseProperties.Health:
                startValue = m_Talents.Defense.Health;
                IncreaseCost = m_Talents.Defense.HealthCost;
                increaseProperty.onClick.AddListener(() => NewHealthParameters(IncreaseCost));
                break;
            case DefenseProperties.HealthRegen:
                startValue = m_Talents.Defense.HealthRegen;
                IncreaseCost = m_Talents.Defense.HealthRegenCost;
                increaseProperty.onClick.AddListener(() => NewHealthRegenParameters(IncreaseCost));
                break;
            case DefenseProperties.Mana:
                startValue = m_Talents.Defense.Mana;
                IncreaseCost = m_Talents.Defense.ManaCost;
                increaseProperty.onClick.AddListener(() => NewManaParameters(IncreaseCost));
                break;
            case DefenseProperties.ManaRegen:
                startValue = m_Talents.Defense.ManaRegen;
                IncreaseCost = m_Talents.Defense.ManaRegenCost;
                increaseProperty.onClick.AddListener(() => NewManaRegenParameters(IncreaseCost));
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
    private float IncreaseValueReturner(DefenseProperties property)
    {
        DefensePropertyIncreaseValue.TryGetValue(property, out float value);
        return GameUtilities.FloatHandler(value);
    }
    private void NewHealthParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Defense.Health = GameUtilities.FloatHandler(Value);
        data.Defense.HealthCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Defense);
    }

    private void NewHealthRegenParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Defense.HealthRegen = GameUtilities.FloatHandler(Value);
        data.Defense.HealthRegenCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Defense);
    }

    private void NewManaParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Defense.Mana = GameUtilities.FloatHandler(Value);
        data.Defense.ManaCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Defense);
    }

    private void NewManaRegenParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Defense.ManaRegen = GameUtilities.FloatHandler(Value);
        data.Defense.ManaRegenCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Defense);
    }

    private void SetTalents(DefenseTalents defenseTalent)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Defense = defenseTalent;
        DatabaseManager.instance.AddGold(-IncreaseCost);
        DatabaseManager.instance.UpdateTalents(data);
    }
}
