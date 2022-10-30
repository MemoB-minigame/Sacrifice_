using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    //Components
    [SerializeField] GameObject explosionEnemy;
    [SerializeField] GameObject explosionWall;
    new private Rigidbody2D rigidbody;
    private int damage;
    private float DestroyTimer = 0;
    GameObject player;
    public int Damage
    {
        get 
        {
            return damage;
        }
        set
        {
            damage = value;
            //damage*=player.GetComponent<PlayerController>().buff;
        }
    }


    void Awake()
    {
        player = GameObject.Find("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        DestroyTimer = 0;
        StartCoroutine(Destroy(3f));//若采用Invoke 在之后再次使用该子弹的时候可能出现bug
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject _explosion = ObjectPool.Instance.GetObject(explosionEnemy);
            _explosion.transform.position = transform.position;
            _explosion.transform.right = transform.right;
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            GameObject _explosion = ObjectPool.Instance.GetObject(explosionWall);
            _explosion.transform.position = transform.position;
            _explosion.transform.right = transform.right;
        }
        if(gameObject.active)
            StartCoroutine(Destroy(0f));
    }

    public void SetBullet(int _damage,float speed, Vector2 direction)
    {
        Damage = _damage;
        rigidbody.velocity = direction.normalized * speed ;
        transform.right = direction;
    }

    
    IEnumerator Destroy(float interval)
    {
        
        while (DestroyTimer <= interval)
        {  
            DestroyTimer+=Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
        yield break;
    }



}
