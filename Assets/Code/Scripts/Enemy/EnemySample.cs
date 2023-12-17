using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySample : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Colpito");
    }

}
