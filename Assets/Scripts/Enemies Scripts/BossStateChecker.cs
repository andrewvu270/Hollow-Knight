using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_State
{
    NONE,
    IDLE,
    PAUSE,
    ATTACK,
    DEATH
}
public class BossStateChecker : MonoBehaviour
{
    private Transform playerTarget;
    private Boss_State boss_State = Boss_State.NONE;
    private float distanceToTarget;

    private EnemyHP bossHealth;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    void SetState()
    {
        distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

        if(boss_State != Boss_State.DEATH)
        {
            if(distanceToTarget > 3 && distanceToTarget <= 15f)
            {
                boss_State = Boss_State.PAUSE;
            } else if (distanceToTarget > 15f)
            {
                boss_State = Boss_State.IDLE;
            } else if (distanceToTarget <= 3f)
            {
                boss_State = Boss_State.ATTACK;
            } else
            {
                boss_State = Boss_State.NONE;
            }

            if(bossHealth.health <= 0f)
            {
                boss_State = Boss_State.DEATH;
            }
        }
    }

    public Boss_State Boss_State
    {
        get { return boss_State; }
        set { boss_State = value; }
    }
}
