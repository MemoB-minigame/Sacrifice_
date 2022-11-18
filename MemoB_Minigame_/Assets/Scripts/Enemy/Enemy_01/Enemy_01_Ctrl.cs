using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01_Ctrl : MonoBehaviour
{
    private BuffManager buffManager;
    private int hp;
    private Animator animator;
    private PlayerController playerController;
    private Enemy_01_Parameters para;
    public int Hp 
    {
        get {return hp; }
        set 
        {
            hp=value;
            if(hp<=0)Die();
        }

    }
    void Awake()
    {

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        para = GetComponent<Enemy_01_Parameters>();
        hp = para.attribute.Hp;
        animator = GetComponent<Animator>();

        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    }
    void Die()
    {
        buffManager.ifLastBulletKills = true;
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("BeAttacked");
            Hp -= collision.gameObject.GetComponent<PlayerBullet>().Damage;
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Aggress"))
                animator.SetTrigger("InterruptSwitch");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector3 attackDirection = collision.transform.position - transform.position;
            collision.gameObject.SendMessage("HurtRecoilForceSpider", new Vector2(attackDirection.x, attackDirection.y));
            playerController.HP -= (int)para.attribute.attackDamage;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator.SetTrigger("AttackBackToAlert");
        }
    }
}
