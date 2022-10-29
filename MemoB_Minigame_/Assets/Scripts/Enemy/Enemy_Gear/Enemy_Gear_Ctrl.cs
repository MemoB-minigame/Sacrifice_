using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Ctrl : MonoBehaviour
{
    [SerializeField]private int hp;
    private Animator animator;
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
    }
    void Start()
    {
        
    }

    void Update()
    {
        animator.SetInteger("Hp", hp);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= collision.gameObject.GetComponent<PlayerBullet>().Damage;
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
