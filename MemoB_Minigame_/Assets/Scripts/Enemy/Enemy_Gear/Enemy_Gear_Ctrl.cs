using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Ctrl : MonoBehaviour
{
    [SerializeField]private int hp;
    private Animator animator;
    private BuffManager buffManager;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0) Die();
        }

    }
    private void Awake()
    {
        hp = GetComponent<Enemy_Gear_Parameters>().attribute.Hp;
        animator = GetComponent<Animator>();
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log(collision.gameObject.GetComponent<PlayerBullet>().Damage);
            Hp -= collision.gameObject.GetComponent<PlayerBullet>().Damage;
        }
    }
    void Die()
    {
        buffManager.ifLastBulletKills = true;
        animator.SetTrigger("Die");
    }
}
