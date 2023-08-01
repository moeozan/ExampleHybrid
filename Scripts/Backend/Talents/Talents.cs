using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public class AttackTalents
{
    [FirestoreProperty] public float Damage { get; set; } = 25f;
    [FirestoreProperty] public float DamageCost { get; set; } = 10f;
    [FirestoreProperty] public float Speed { get; set; } = 1.5f;
    [FirestoreProperty] public float SpeedCost { get; set; } = 10f;
    [FirestoreProperty] public float Range { get; set; } = 5f;
    [FirestoreProperty] public float RangeCost { get; set; } = 10f;
    [FirestoreProperty] public float DamagePerMeter { get; set; } = 3f;
    [FirestoreProperty] public float DamagePerMeterCost { get; set; } = 10f;
    [FirestoreProperty] public float Critical { get; set; } = 10f;
    [FirestoreProperty] public float CriticalCost { get; set; } = 20f;
    [FirestoreProperty] public float Multiplier { get; set; } = 1.5f;
    [FirestoreProperty] public float MultiplierCost { get; set; } = 20f;

    public AttackTalents() { }
}

[FirestoreData]
public class DefenseTalents
{
    [FirestoreProperty] public float Health { get; set; } = 150f;
    [FirestoreProperty] public float HealthCost{ get; set; } = 5f;
    [FirestoreProperty] public float HealthRegen { get; set; } = 5f;
    [FirestoreProperty] public float HealthRegenCost { get; set; } = 5f;
    [FirestoreProperty] public float Mana { get; set; } = 100f;
    [FirestoreProperty] public float ManaCost { get; set; } = 5f;
    [FirestoreProperty] public float ManaRegen { get; set; } = 5f;
    [FirestoreProperty] public float ManaRegenCost { get; set; } = 5f;

    public DefenseTalents() { }
}

[FirestoreData]
public class UtilityTalents
{
    [FirestoreProperty] public float GoldPerEnemy { get; set; } = 1f;
    [FirestoreProperty] public float GoldPerEnemyCost { get; set; } = 3f;
    [FirestoreProperty] public float ExpPerEnemy { get; set; } = 1f;
    [FirestoreProperty] public float ExpPerEnemyCost { get; set; } = 3f;
    [FirestoreProperty] public float GoldPerLevel { get; set; } = 5f;
    [FirestoreProperty] public float GoldPerLevelCost { get; set; } = 3f;
    [FirestoreProperty] public float ExpPerLevel { get; set; } = 5f;
    [FirestoreProperty] public float ExpPerLevelCost { get; set; } = 3f;

    public UtilityTalents() { }
}

[FirestoreData]
public class Talents
{
    [FirestoreProperty] public AttackTalents Attack { get; set; } = new AttackTalents();

    [FirestoreProperty] public DefenseTalents Defense { get; set; } = new DefenseTalents();

    [FirestoreProperty] public UtilityTalents Utility{ get; set; } = new UtilityTalents();
}

