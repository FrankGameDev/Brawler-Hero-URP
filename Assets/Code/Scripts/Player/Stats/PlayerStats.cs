using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player stats", menuName = "My Object/Player stats")]
public class PlayerStats : ScriptableObject
{
    public int hp;

    [Header("Grounded fields")]
    public float walkingSpeed;
    public float runningSpeed;
    public float jumpForce;

    [Header("Fly locomotion fields")]
    public float normalFlySpeed;
    public float maxFlySpeed;
    public float maxFlyDiveSpeed;
    public float timeToMaxSpeed;

    [Header("Dodge parameters")]
    public float dodgeSpeed;
    public float dodgeCooldown;
}
