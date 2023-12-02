using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player Combat Stats", menuName = "My Object/Player Combat Stats")]
public class PlayerCombatStats : ScriptableObject
{
    [Header("Light attack parameters")]
    public int lightAttackDmg;
    public float attackCooldown;

    [Header("Long distance attack parameters")]
    [Tooltip("Grandezza dell'area di rilevamento di un nemico")] public float detectLongRangeAttackRadius;
    [Tooltip("Distanza massima per eseguire il colpo a distanza")] public float maxAttackRange;
    [Tooltip("Distanza minima per eseguire il colpo a distanza")]public float minAttackRange;

    [Header("Grab parameters and utility")]
    public float grabForce;
    [Header("Throw fields")]
    public float throwForce;
}
