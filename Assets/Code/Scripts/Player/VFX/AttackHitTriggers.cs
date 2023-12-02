using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackHitTriggers : MonoBehaviour
{

    public bool haveHit { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.CompareTag("Player"))
            return;

        haveHit = other.CompareTag("Enemy");
    }

    private void OnTriggerExit(Collider other)
    {
        haveHit = false;
    }
}
