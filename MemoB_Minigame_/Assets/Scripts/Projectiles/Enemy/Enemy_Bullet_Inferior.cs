using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Inferior : Enemy_Bulllet_Basic
{
    [Header("ƫ�ƽǶ�")]
    [SerializeField] float inferiorBulletAngle;
    [Header("�ӵ�����")]
    [SerializeField] GameObject inferiorBulletMode;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision happens");
        if (collision.gameObject.CompareTag("Border") && blockable)//FIXME: ����Ϊǽ�ڵ�tag
        {
            Vector2 inferiorDirection = Quaternion.AngleAxis(inferiorBulletAngle, Vector3.forward) * transform.right;
            GameObject bullet = ObjectPool.Instance.GetObject(inferiorBulletMode);
            bullet.transform.position = transform.position;
            bullet.transform.right = inferiorDirection; 


            if (gameObject.active)//��ֹ��bug
                StartCoroutine(Destroy(0f));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            playerController.HP -= damage;
            if (gameObject.active)//��ֹ��bug
                StartCoroutine(Destroy(0f));
        }
    }
}
