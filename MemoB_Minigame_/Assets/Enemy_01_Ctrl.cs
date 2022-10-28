using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01_Ctrl : MonoBehaviour
{
    private int hp;
    private Animator animator;
    public int Hp 
    {
        get {return hp; }
        set 
        {
            hp=value;
            if(hp<=0)Die();
        }

    }
    void Start()
    {
        hp = GetComponent<Enemy_01_Parameters>().attribute.Hp;
        animator = GetComponent<Animator>();
    }
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Hp -= collision.gameObject.GetComponent<RevolverBullet>().damage;
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Aggress"))
                animator.SetTrigger("InterruptSwitch");
        }
    }
}
