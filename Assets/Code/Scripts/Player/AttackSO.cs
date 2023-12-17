using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Combat/AttackSO")]
public class AttackSO : ScriptableObject
{
    public AttackType Type;
    public string AnimationName;
}
