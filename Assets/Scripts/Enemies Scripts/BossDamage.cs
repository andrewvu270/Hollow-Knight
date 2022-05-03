using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public LayerMask playerLayer;
    public float radius = 0.3f;
    public float damageCount = 10f;

    private PlayerHealth playerHealth;
    private bool collided;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);

        foreach(Collider c in hits)
        {
            playerHealth = c.gameObject.GetComponent<PlayerHealth>();
            collided = true;
        }

        if (collided)
        {
            playerHealth.TakeDamage(damageCount);
            enabled = false;
        }
    }
}
