using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bullet_Basic : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] protected float speed;             // �����ٶ�
    [SerializeField] protected float prepareDuration;   // ��Ļ׼��ʱ�䣬׼��ʱ���ڵ�Ļ���ƶ�
    [SerializeField] protected float lifeDuration;      // ��������
    [SerializeField] protected int damage;              // ��Ļ�˺�
    [Header("��ײ")]
    [SerializeField] protected bool blockable;          // �Ƿ�ɱ��ϰ����赲
    [SerializeField] protected float repelPower;        // ����������δ����
    [Header("׷��")]
    protected Transform trackTarget;   // ׷�ٶ���
    [SerializeField] protected float trackPower;        // ׷��ǿ�ȣ�����Ϊ1.0ʱ��1���ڶ�׼Ŀ�꣩
    [Header("��¼")]
    [SerializeField] protected float lifeTimer;         // ������ʱ��
    [SerializeField] protected Queue<Vector3> trace;    // ��Ļ·�������
    [SerializeField] protected float traceDuration;     // ��¼·�����ʱ����
    [SerializeField] protected int traceMaxLength;      // �����·�����¼��

    protected float traceTimer;     // ·����¼��ʱ��
    protected PlayerController playerController;
    private float preTimer;
    //���ݲ������
    protected struct Pack 
    {
        public bool inheritSpeed;
        public bool inheritDamage;
        public bool inheritTrackPower;
        public float speed;
        public float damage;
        public float trackPower;

        

        public Pack(bool inheritSpeed, bool inheritDamage, bool inheritTrackPower, float speed, float damage, float trackPower)
        {
            this.inheritSpeed = inheritSpeed;
            this.inheritDamage = inheritDamage;
            this.inheritTrackPower = inheritTrackPower;
            this.speed = speed;
            this.damage = damage;
            this.trackPower = trackPower;
        }
    }
    

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
        if (trackTarget == null) { return; }
        Vector2 dirToTarget = (trackTarget.position - transform.position).normalized;
        transform.right = Vector2.Lerp(transform.right, dirToTarget, trackPower*Time.deltaTime).normalized;
    }
    public virtual void SetBullet(float _speed)
    {
        speed = _speed;
    }
    public virtual void SetBullet(int _damage, float _speed,float _trackPower)
    {
        speed = _speed;
        damage = _damage;
        trackPower = _trackPower;
    }
    protected virtual void SetBullet(Pack pack)//���ڼ̳��ӵ��Ĵ������
    {
        if (pack.inheritSpeed)
            speed = pack.speed;
        if (pack.inheritDamage)
            speed = pack.speed;
        if(pack.inheritTrackPower)
            trackPower = pack.trackPower;
    }
    protected virtual bool Prepare()
    {
        return preTimer >= prepareDuration;
    }

   
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border")&&blockable)//FIXME: ����Ϊǽ�ڵ�tag
        {
            if(gameObject.active)//��ֹ��bug
                StartCoroutine(Destroy(0f));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            playerController.HP -= damage;
            playerController.HurtRecoilForce(repelPower, new Vector2(transform.right.x,transform.right.y));
            if (gameObject.active)//��ֹ��bug
                StartCoroutine(Destroy(0f));
        }
    }
    protected IEnumerator Destroy(float interval)
    {
        float destroyTimer = 0f;
        while (destroyTimer <= interval)
        {
            destroyTimer += Time.deltaTime;
            //Debug.Log(destroyTimer);
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
        yield break;
    }
}
