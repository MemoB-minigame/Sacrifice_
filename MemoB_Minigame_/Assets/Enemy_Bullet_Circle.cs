using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bullet_Circle : Enemy_Bulllet_Basic
{
    [Header("标量")]
    [SerializeField] float angel;
    [SerializeField] float basicRadius;
    [SerializeField] int bulletNum;
    [SerializeField] float changeDuration;
    [Header("子弹基底")]
    [SerializeField] GameObject bulletMode;

    bool generate;
    float changeTimer;
    protected override void OnEnable()
    {
        base.OnEnable();
        generate = false;
        changeTimer = 0;    
    }
    protected override void Update()
    {
        changeTimer += Time.deltaTime;
        base.Update();
        if (changeTimer>=changeDuration&&!generate)
        {
            Generate();
        }
    }
    protected virtual void Generate()
    {
        generate = true;
        if (angel > 360) angel %= 360;
        float _angel =angel/ bulletNum;
        int mid = bulletNum / 2;
        if (bulletNum % 2 == 0)
        {
            
            for (int i = 1; i <= bulletNum; i++)
            {
                GameObject bullet = ObjectPool.Instance.GetObject(bulletMode);
                bullet.transform.position = transform.position;
                bullet.transform.right = Quaternion.AngleAxis((i-mid-0.5f)*_angel,Vector3.forward)*transform.right;
                bullet.transform.position += bullet.transform.right.normalized * basicRadius;
            }
        }
        else
        {
            for (int i = 1; i <= bulletNum; i++)
            {
                GameObject bullet = ObjectPool.Instance.GetObject(bulletMode);
                bullet.transform.position = transform.position;
                bullet.transform.right = Quaternion.AngleAxis((i - mid-1) * _angel, Vector3.forward) * transform.right;
                bullet.transform.position+=bullet.transform.right.normalized*basicRadius;
            }
        }
        StartCoroutine(Destroy(0f));
    }
}
