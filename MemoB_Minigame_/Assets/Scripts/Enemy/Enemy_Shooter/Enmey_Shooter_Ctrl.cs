using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey_Shooter_Ctrl : MonoBehaviour
{
    GameObject player;
    Animator animator;
    [Header("µ–»À Ù–‘")]
    [SerializeField]int hp;
    [SerializeField] int recover;
    
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
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Die()
    {
        animator.SetTrigger("Die");
        player.GetComponent<PlayerController>().HP += recover;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")){
            HP-=collision.GetComponent<PlayerBullet>().Damage;
        }
    }
    IEnumerator WaitForDeath()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime<=1f)
        {
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
