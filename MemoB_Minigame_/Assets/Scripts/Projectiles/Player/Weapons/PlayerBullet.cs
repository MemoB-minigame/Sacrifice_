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
    //buff���
    private BuffManager buffManager;
    private float scaleX,scaleY;    
    private bool penetration;
    private float scaleMutiple;
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

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }
    private void OnEnable()
    {
        scaleMutiple = 1;
        DestroyTimer = 0;
        InstallBuffs();
        StartCoroutine(Destroy(3f));//������Invoke ��֮���ٴ�ʹ�ø��ӵ���ʱ����ܳ���bug
    }

    public void SetBullet(int _damage,float speed, Vector2 direction)
    {
        Damage = _damage;
        rigidbody.velocity = direction.normalized * speed ;
        transform.right = direction;
    }
    protected virtual void InstallBuffs()
    {
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();

        penetration = buffManager.buffs[1];
        scaleMutiple = buffManager.bulletScaleMutiple;
        if (buffManager.buffs[2])
            transform.localScale = new Vector3(scaleX * scaleMutiple, scaleY * scaleMutiple, 1);
        //if (buffManager.buffs[3] && buffManager.ifLastBulletKills)
        //{
        //    buffManager.ifLastBulletKills = false;
        //    damageMutiple = 2;
        //}
        //����÷ŵ�ǹе�����ж�
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject _explosion = ObjectPool.Instance.GetObject(explosionEnemy);
            _explosion.transform.position = transform.position;
            _explosion.transform.right = transform.right;
            if (gameObject.active && !penetration)
                StartCoroutine(Destroy(0f));
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            GameObject _explosion = ObjectPool.Instance.GetObject(explosionWall);
            _explosion.transform.position = transform.position;
            _explosion.transform.right = transform.right;
            if (gameObject.active)
                StartCoroutine(Destroy(0f));
        }
        
    }
    IEnumerator Destroy(float interval)
    {

        while (DestroyTimer <= interval)
        {
            DestroyTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
        yield break;
    }
}
