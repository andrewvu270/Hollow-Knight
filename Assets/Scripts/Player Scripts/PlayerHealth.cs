using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private bool isShielded;

    private Animator anim;

    private Image health_Img;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        health_Img = GameObject.Find("Health Icon").GetComponent<Image>();
    }

    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    public void TakeDamage(float amount)
    {
        if (!isShielded)
        {
            health -= amount;
            health_Img.fillAmount = health/100f;
            print("Player tool dmg " + amount);
            print("Health is " + health);
            if (health <= 0f)
            {
                anim.SetBool("Death", true);

                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                {
                    Destroy(gameObject, 1f);
                }
            }
        }
    }

    public void HealPlayer(float healAmount)
    {
        health += healAmount;

        if(health > 100f)
        {
            health = 100f;
        }

        health_Img.fillAmount = health / 100f;

    }

}
