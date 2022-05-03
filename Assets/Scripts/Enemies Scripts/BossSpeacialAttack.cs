using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeacialAttack : MonoBehaviour
{
    public GameObject bossFire;
    public GameObject bossMagic;

    private Transform playerTarget;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void BossFireTornado()
    {
        Instantiate(bossFire, playerTarget.position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));

    }

    void BossSpell()
    {
        Vector3 temp = playerTarget.position;
        temp.y += 1.5f;
        Instantiate(bossMagic, temp, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
