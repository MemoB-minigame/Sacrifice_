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
    [Header("敌人状态")]
    [SerializeField]int hp;
    Enemy_Shooter_Parameters para;
    
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < hp && value != 0)
                if(ifHasHurtAni)
                    SendMessage("HurtAni");
            hp = value;
            if (hp <= 0 && firstDie) 
            {
                firstDie = false;
                Die(); 
            }
        }
        
    }
    private void Awake()
    {
        para = GetComponent<EnemyShooterController>();
        if(para==null)
            para = GetComponent<Enemy_ShooterB2_Parameters>();
        HP = para.Hp;
        firstDie=true;
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
