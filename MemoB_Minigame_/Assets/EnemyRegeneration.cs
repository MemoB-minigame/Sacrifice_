using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyRegeneration : MonoBehaviour
{
    [Tooltip("�����뾶")]
    [SerializeField] float attractRadius;
    [Tooltip("���������ٶ�")]
    [SerializeField] float followSpeed;
    [Tooltip("�ָ�Ѫ��")]
    public int regeneration;
    new Rigidbody2D rigidbody;
    private bool attractable;//�ɱ�����
    private bool attracting;//���ڱ�������

    //player
    GameObject player;
    private void OnEnable()
    {
        if(rigidbody==null)
            rigidbody = GetComponent<Rigidbody2D>();
        if(player==null)
            player = GameObject.FindGameObjectWithTag("Player");

        rigidbody.gravityScale = 0;
        attractable = false;
        attracting = false;
        StartCoroutine(SpawnGeneration());
    }
    private void Update()
    {
        if (attractable)
        {
            ToBeAttracted();
        }
        if (attracting)
        {
            BeingAttracting();
        }
    }
    private void ToBeAttracted()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attractRadius)
        {
            attractable = false;
            attracting = true;
        }
    }
    private void BeingAttracting()
    {
        if(Vector3.Distance(player.transform.position, transform.position)>0.1f)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime * 5);
        else
        {
            int tmpHp = player.GetComponent<PlayerController>().HP;
            player.GetComponent<PlayerController>().HP = tmpHp + regeneration >= 24 ? 24 : tmpHp + regeneration;
            StartCoroutine(Destroy(0f));
        }
    }
    IEnumerator SpawnGeneration()//ģ�����Ч��
    {
        rigidbody.gravityScale = 3;
        rigidbody.velocity = Vector2.up * 5f;
        Vector3 targetPos = transform.position + Vector3.down*0.7f;

        while (transform.position.y>targetPos.y)
            yield return new WaitForFixedUpdate();

        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector2.zero;
        attractable = true;//�����ڵ���֮��ʼ���Ա�����
    }
    IEnumerator Destroy(float interval)//Ϊ����������Ƶ�destroy
    {
        float destroyTimer = 0f;
        while (destroyTimer <= interval)
        {
            destroyTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
        yield break;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractRadius);
    }

}
