using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Queue : Enemy_Bullet_Basic
{
    [Header("◊”µØ…Ë÷√")]
    [SerializeField] GameObject bulletMode;
    [SerializeField] GameObject[] bulletsPos;

    protected override void OnEnable()
    {
        base.OnEnable();
        Generate();
        StartCoroutine(Destroy(0f));
    }
    void Generate()
    {
        for (int i = 0; i < bulletsPos.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletMode);
            bullet.transform.position = bulletsPos[i].transform.position;
            bullet.transform.right = transform.right;
        }
    }
}
