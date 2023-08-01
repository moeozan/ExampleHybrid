using System.Collections;
using UnityEngine;

public class AttackProperty: Property
{
    [SerializeField] private AttackProperties m_Property;

    public override void Awake()
    {
        increaseValue = IncreaseValueReturner(m_Property);
        propertyName.text = m_Property.ToString();
    }

    public override void Start()
    {
        StartCoroutine(Wake());
        base.Start();
    }

    private IEnumerator Wake()
    {
        yield return new WaitUntil(() => DatabaseManager.instance.Data != null);
        m_Talents = DatabaseManager.instance.GetTalents();
        increaseValue = IncreaseValueReturner(m_Property);
        switch (m_Property)
        {
            case AttackProperties.Damage:
                startValue = m_Talents.Attack.Damage;
                increaseProperty.onClick.AddListener(() => PropertyManager.DamageHandle.Invoke(increaseValue));
                break;
            case AttackProperties.Speed:
                startValue = m_Talents.Attack.Speed;
                increaseProperty.onClick.AddListener(() => PropertyManager.SpeedHandle.Invoke(increaseValue));
                break;
            case AttackProperties.Range:
                startValue = m_Talents.Attack.Range;
                increaseProperty.onClick.AddListener(() => PropertyManager.RangeHandle.Invoke(increaseValue));
                break;
            case AttackProperties.DamagePerMeter:
                startValue = m_Talents.Attack.DamagePerMeter;
                increaseProperty.onClick.AddListener(() => PropertyManager.DamagePerRangeHandle.Invoke(increaseValue));
                break;
            case AttackProperties.Critical:
                startValue = m_Talents.Attack.Critical;
                increaseProperty.onClick.AddListener(() => PropertyManager.CriticalHandle.Invoke(increaseValue));
                break;
            case AttackProperties.Multiplier:
                startValue = m_Talents.Attack.Multiplier;
                increaseProperty.onClick.AddListener(() => PropertyManager.MultiplierHandle.Invoke(increaseValue));
                break;
        }
        base.Awake();
    }

    private float IncreaseValueReturner(AttackProperties property)
    {
        AttackPropertyIncreaseValue.TryGetValue(property, out float value);
        return value;
    }
}
