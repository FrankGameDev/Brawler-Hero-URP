using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground Enemy Parameters", menuName = "My Object/Ground Enemy Parameters")]
public class GroundEnemyParameters : ScriptableObject
{
    public int hp;

    public float speed;

    public int damage;
}
