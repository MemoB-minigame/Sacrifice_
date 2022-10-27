using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01_Ctrl : MonoBehaviour
{
    [SerializeField]private int hp;
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
    }
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("attacked");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp-=collision.gameObject.GetComponent<RevolverBullet>().damage;
        }
    }
}
