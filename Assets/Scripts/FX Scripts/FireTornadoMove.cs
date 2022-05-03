using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoMove : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;
    public GameObject fireExplosion;

    private EnemyHP enemyHealth;
    private bool collided;

    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDamage();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach(Collider c in hits)
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHP>();
            collided = true;
        }

        if (collided)
        {
            enemyHealth.TakeDamage (damageCount);
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
