using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public float health = 100f;

    public Image health_Img;

    private void Awake()
    {
/*        if(tag == "Boss")
        {
            health_Img = GameObject.Find("HP Foreground Boss").GetComponent<Image>();
        } else
        {
            health_Img = GameObject.Find("HP Foreground").GetComponent<Image>();
        }*/
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        health_Img.fillAmount = health / 100f;
        print("Enemy took dmg. HP is " + health);

        if(health <= 0)
        {

        }
    }
}
