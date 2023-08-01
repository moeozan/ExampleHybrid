using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DefenseProperty : Property
{
    [SerializeField] private DefenseProperties m_Property;

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

    private IEnumerator Wake()
    {
        yield return new WaitUntil(() => DatabaseManager.instance.Data != null);
        m_Talents = DatabaseManager.instance.GetTalents();
        increaseValue = IncreaseValueReturner(m_Property);
        switch (m_Property)
        {
            case DefenseProperties.Health:
                startValue = m_Talents.Defense.Health;
                increaseProperty.onClick.AddListener(() => PropertyManager.HealthHandle.Invoke(increaseValue));
                break;
            case DefenseProperties.HealthRegen:
                startValue = m_Talents.Defense.HealthRegen;
                increaseProperty.onClick.AddListener(() => PropertyManager.HealthRegenHandle.Invoke(increaseValue));
                break;
            case DefenseProperties.Mana:
                startValue = m_Talents.Defense.Mana;
                increaseProperty.onClick.AddListener(() => PropertyManager.ManaHandle.Invoke(increaseValue));
                break;
            case DefenseProperties.ManaRegen:
                startValue = m_Talents.Defense.ManaRegen;
                increaseProperty.onClick.AddListener(() => PropertyManager.ManaRegenHandle.Invoke(increaseValue));
                break;
        }
        base.Awake();
    }

    private float IncreaseValueReturner(DefenseProperties property)
    {
        DefensePropertyIncreaseValue.TryGetValue(property, out float value);
        return value;
    }
}
