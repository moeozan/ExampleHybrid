using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private EnemyPooling enemyPooling;

    public int Level { get; private set; }
    public float LevelExperince { get; private set; }
    public float MonsterExperience { get; private set; }
    public float CurrentExperience { get; private set; }
    public float ExpBonus { get; private set; }
    public float MonsterDamageMultiplier { get; private set; }
    public float MonsterHealthMultiplier { get; private set; }

    private bool levelKey;
    private List<Coin> coins= new();

    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
        LevelExperince = 10;
        Level = 1;
        MonsterExperience = 1;
        ExpBonus = 1;
        MonsterDamageMultiplier = .1f;
        MonsterHealthMultiplier = .025f;
    }

    public void HardnessSetter()
    {
        if (!levelKey) { return; }
        if (Level != 0 && Level %5 == 0)
        {
            ExpBonus = (Level / 5) + 1;
            LevelExperince *= 2.2f;
            LevelExperince = GameUtilities.FloatHandler(LevelExperince);
            MonsterExperience *= 2;
            MonsterExperience = GameUtilities.FloatHandler(MonsterExperience);
            MonsterDamageMultiplier += .2f;
            EnemyPooling.instance.IncreaseEnemyAmount(1);
            levelKey = false;
        }
    }

    public void ExperienceChecker()
    {
        CurrentExperience += MonsterExperience;
        levelUI.IncreaseLevelExperience(CurrentExperience, LevelExperince);
        if (CurrentExperience >= LevelExperince)
        {
            Level++;
            coins = FindObjectsOfType<Coin>().ToList();
            foreach (var c in coins)
            {
                c.Loot();
            }
            coins.Clear();
            MonsterHealthMultiplier += .025f;
            foreach (var monster in enemyPooling.EnemiesInPool)
            {
                monster.GetComponent<PoolEnemy>().MaxHealth += monster.GetComponent<PoolEnemy>().MaxHealth * MonsterHealthMultiplier;
            }
            levelUI.IncreaseLevelExperience(CurrentExperience, LevelExperince);
            CurrentExperience -= LevelExperince;
            Debug.Log("LEVEL: " + Level);
            PropertyManager.ExpHandle(PropertyManager.instance.ExperincePerLevel);
            PropertyManager.GoldHandle(PropertyManager.instance.GoldPerLevel);
            levelKey = true;
            HardnessSetter();
        }
    }
}
