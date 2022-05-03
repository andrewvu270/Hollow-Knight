using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossControl : MonoBehaviour
{
    private Transform playerTarget;
    private BossStateChecker bossStateChecker;
    private NavMeshAgent navAgent;
    private Animator anim;

    private bool finishedAttacking = true;
    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossStateChecker>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedAttacking)
        {
            GetStateControl();
        }
        else
        {
            anim.SetInteger("Atk", 0);
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishedAttacking = true;
            }
        }
    }

    void GetStateControl()
    {
        if(bossStateChecker.Boss_State == Boss_State.DEATH)
        {
            navAgent.isStopped = true;
            anim.SetBool("Death", true);
            Destroy(gameObject, 3f);
        } else
        {
            if(bossStateChecker.Boss_State == Boss_State.PAUSE)
            {
                navAgent.isStopped = false;
                anim.SetBool("Run", true);

                navAgent.SetDestination(playerTarget.position);
            } else if (bossStateChecker.Boss_State == Boss_State.ATTACK)
            {
                anim.SetBool("Run", false);
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);

                if(currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 5);
                    anim.SetInteger("Atk", atkRange);

                    currentAttackTime = 0f;
                    finishedAttacking = false;
                } else
                {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            } else
            {
                anim.SetBool("Run", false);
                navAgent.isStopped = true;
            }
        }
    }
}
