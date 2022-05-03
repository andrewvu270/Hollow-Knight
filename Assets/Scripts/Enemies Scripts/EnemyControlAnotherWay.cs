using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlAnotherWay : MonoBehaviour
{
    public Transform[] walkPoints;
    private int walk_Index = 0;

    private Transform playerTarget;

    private Animator anim;
    private NavMeshAgent navAgent;

    private float walk_Distance = 8f;
    private float attack_Distance = 2f;

    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private Vector3 nextDestination;

    private EnemyHP enemyHealth;
    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.health > 0)
        {
            MoveAndAttack();
        } else
        {
            anim.SetBool("Death", true);
            navAgent.enabled = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }
    }

    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance > walk_Distance)
        {
            if (navAgent.remainingDistance <= 0.5f)
            {
                navAgent.isStopped = false;

                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetInteger("Atk", 0);

                nextDestination = walkPoints[walk_Index].position;
                navAgent.SetDestination(nextDestination);

                if (walk_Index == walkPoints.Length - 1)
                {
                    walk_Index = 0;
                }
                else
                {
                    walk_Index++;
                }
            }
        }
        else
        {
            if (distance > attack_Distance)
            {
                navAgent.isStopped = false;

                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                anim.SetInteger("Atk", 0);

                navAgent.SetDestination(playerTarget.position);
            }
            else
            {
                navAgent.isStopped = true;

                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);

                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);

                if (currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 3);
                    anim.SetInteger("Atk", atkRange);
                    currentAttackTime = 0f;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            }
        }
    }
}
