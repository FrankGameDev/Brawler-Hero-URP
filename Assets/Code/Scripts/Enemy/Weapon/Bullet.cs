using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null || !collision.collider.CompareTag("Player"))
            return;

        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.TakeDamage(1);
        Destroy(gameObject);
    }
}
