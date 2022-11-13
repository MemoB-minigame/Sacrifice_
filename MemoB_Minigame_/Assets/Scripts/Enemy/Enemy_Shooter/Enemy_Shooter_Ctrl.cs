using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter_Ctrl : MonoBehaviour
{
    BuffManager buffManager;
    GameObject player;
    Animator animator;
    [Header("µÐÈË×´Ì¬")]
    [SerializeField]int hp;
    
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp <= 0) Die();
        }
        
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    }

    void Die()
    {
        animator.SetTrigger("Die");
        buffManager.ifLastBulletKills = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")){
            animator.SetTrigger("BeAttacked");
            HP -=collision.GetComponent<PlayerBullet>().Damage;
        }
    }
}
