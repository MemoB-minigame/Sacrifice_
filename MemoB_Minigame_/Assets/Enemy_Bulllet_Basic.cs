using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bulllet_Basic : MonoBehaviour
{
    [Header("基本设置")]
    [SerializeField] protected float speed;             // 弹道速度
    [SerializeField] protected float prepareDuration;   // 弹幕准备时间，准备时间内弹幕不移动
    [SerializeField] protected float lifeDuration;      // 总生命期
    [SerializeField] protected int damage;              // 弹幕伤害
    [Header("碰撞")]
    [SerializeField] protected bool blockable;          // 是否可被障碍物阻挡
    [SerializeField] protected float repelPower;        // 击退力，暂未定义
    [Header("追踪")]
    protected Transform trackTarget;   // 追踪对象
    [SerializeField] protected float trackPower;        // 追踪强度（设置为1.0时在1秒内对准目标）
    [Header("记录")]
    [SerializeField] protected float lifeTimer;         // 生命计时器
    [SerializeField] protected Queue<Vector3> trace;    // 弹幕路径点队列
    [SerializeField] protected float traceDuration;     // 记录路径点的时间间隔
    [SerializeField] protected int traceMaxLength;      // 最大保留路径点记录数

    protected float traceTimer;     // 路径记录计时器
    protected PlayerController playerController;
    private float preTimer;
    protected virtual void Awake()
    {
        trackTarget = GameObject.Find("Player").transform;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    protected virtual void OnEnable()
    {
        preTimer = 0;
        StartCoroutine(Destroy(lifeDuration));
    }
    protected virtual void Update()
    {
        preTimer += Time.deltaTime;
        if (Prepare())
        {
            Track();
            Move(); 
        }
    }
    protected virtual void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    protected virtual void Track()
    {
        Vector2 dirToTarget = (trackTarget.position - transform.position).normalized;
        transform.right = Vector2.Lerp(transform.right, dirToTarget, trackPower*Time.deltaTime).normalized;
    }
    protected virtual void SetBullet(Vector2 _direction,float _speed, int _damage)
    {
        transform.right = _direction;
        damage = _damage;
        speed = _speed;
    }
    protected virtual bool Prepare()
    {
        return preTimer >= prepareDuration;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border")&&blockable)//FIXME: 更换为墙壁的tag
        {
            if(gameObject.active)//防止出bug
                StartCoroutine(Destroy(0f));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            playerController.HP -= damage;
            if (gameObject.active)//防止出bug
                StartCoroutine(Destroy(0f));
        }
    }
    protected IEnumerator Destroy(float interval)
    {
        float destroyTimer = 0f;
        while (destroyTimer <= interval)
        {
            destroyTimer += Time.deltaTime;
            Debug.Log(destroyTimer);
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
        yield break;
    }
}
