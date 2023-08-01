using System.Collections;
using UnityEngine;

public class UtilityProperty : Property
{
    [SerializeField] private UtilityProperties m_Property;

    public override void Awake()
    {
        increaseValue = IncreaseValueReturner(m_Property);
        propertyName.text = m_Property.ToString();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(Wake());
    }

    public IEnumerator Wake()
    {
        yield return new WaitUntil(() => DatabaseManager.instance.Data != null);
        m_Talents = DatabaseManager.instance.GetTalents();
        increaseValue = IncreaseValueReturner(m_Property);
        switch (m_Property)
        {
            case UtilityProperties.ExpPerEnemy:
                startValue = m_Talents.Utility.ExpPerEnemy;
                increaseProperty.onClick.AddListener(() => PropertyManager.ExpPerEnemyHandle.Invoke(increaseValue));
                break;

            case UtilityProperties.ExpPerLevel:
                startValue = m_Talents.Utility.ExpPerLevel;
                increaseProperty.onClick.AddListener(() => PropertyManager.ExpPerLevelHandle.Invoke(increaseValue));
                break;

            case UtilityProperties.GoldPerEnemy:
                startValue = m_Talents.Utility.GoldPerEnemy;
                increaseProperty.onClick.AddListener(() => PropertyManager.GoldPerEnemyHandle.Invoke(increaseValue));
                break;

            case UtilityProperties.GoldPerLevel:
                startValue = m_Talents.Utility.GoldPerLevel;
                increaseProperty.onClick.AddListener(() => PropertyManager.GoldPerLevelHandle.Invoke(increaseValue));
                break;
        }
        base.Awake();
    }

    public float IncreaseValueReturner(UtilityProperties property)
    {
        UtilityPropertyIncreaseValue.TryGetValue(property, out float value);
            return value;
    }
}
