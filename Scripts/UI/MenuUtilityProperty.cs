using System.Collections;
using UnityEngine;

public class MenuUtilityProperty : MenuProperty
{
    [SerializeField] private UtilityProperties m_Property;

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
            case UtilityProperties.GoldPerEnemy:
                startValue = m_Talents.Utility.GoldPerEnemy;
                IncreaseCost = m_Talents.Utility.GoldPerEnemyCost;
                increaseProperty.onClick.AddListener(() => NewGoldPerEnemyParameters(IncreaseCost));
                break;
            case UtilityProperties.ExpPerEnemy:
                startValue = m_Talents.Utility.ExpPerEnemy;
                IncreaseCost = m_Talents.Utility.ExpPerEnemyCost;
                increaseProperty.onClick.AddListener(() => NewExpPerEnemyParameters(IncreaseCost));
                break;
            case UtilityProperties.GoldPerLevel:
                startValue = m_Talents.Utility.GoldPerLevel;
                IncreaseCost = m_Talents.Utility.GoldPerLevelCost;
                increaseProperty.onClick.AddListener(() => NewGoldPerLevelParameters(IncreaseCost));
                break;
            case UtilityProperties.ExpPerLevel:
                startValue = m_Talents.Utility.ExpPerLevel;
                IncreaseCost = m_Talents.Utility.ExpPerLevelCost;
                increaseProperty.onClick.AddListener(() => NewExpPerLevelParameters(IncreaseCost));
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
    private float IncreaseValueReturner(UtilityProperties property)
    {
        UtilityPropertyIncreaseValue.TryGetValue(property, out float value);
        return GameUtilities.FloatHandler(value);
    }
    private void NewGoldPerEnemyParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Utility.GoldPerEnemy = GameUtilities.FloatHandler(Value);
        data.Utility.GoldPerEnemyCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Utility);
    }

    private void NewExpPerEnemyParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Utility.ExpPerEnemy = GameUtilities.FloatHandler(Value);
        data.Utility.ExpPerEnemyCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Utility);
    }

    private void NewGoldPerLevelParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Utility.GoldPerLevel = GameUtilities.FloatHandler(Value);
        data.Utility.GoldPerLevelCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Utility);
    }

    private void NewExpPerLevelParameters(float newCost)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Utility.ExpPerLevel = GameUtilities.FloatHandler(Value);
        data.Utility.ExpPerLevelCost = GameUtilities.FloatHandler(newCost);
        SetTalents(data.Utility);
    }

    private void SetTalents(UtilityTalents utilityTalent)
    {
        Talents data = DatabaseManager.instance.Data.Talents;
        data.Utility = utilityTalent;
        DatabaseManager.instance.AddGold(-IncreaseCost);
        DatabaseManager.instance.UpdateTalents(data);
    }
}
