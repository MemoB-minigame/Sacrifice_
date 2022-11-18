using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter_Ctrl : MonoBehaviour
{
    bool firstDie=true;
    [Header("是否具有受击动画")]
    [SerializeField] bool ifHasHurtAni=false;
    BuffManager buffManager;
    GameObject player;
    Animator animator;
    Enemy_Shooter_Parameters param;


    public int HP
    {
        get
        {
            return param.Hp;
        }
        set
        {
            if (value < param.Hp && value != 0)
                if(ifHasHurtAni)
                    SendMessage("HurtAni");
            param.Hp = value;
            if (param.Hp <= 0) Die();
        }
        
    }
    private void Awake()
    {
        firstDie=true;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        param = GetComponent<Enemy_Shooter_Parameters>();
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
