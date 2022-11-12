using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bullet_Circle : Enemy_Bulllet_Basic
{
    [Header("±êÁ¿")]
    [SerializeField] float angle;
    [SerializeField] float basicRadius;
    [SerializeField] int bulletNum;
    [SerializeField] float changeDuration;
    [Header("×Óµ¯»ùµ×")]
    [SerializeField] GameObject bulletMode;
    [Header("×Óµ¯»ùµ×¼Ì³ÐÊôÐÔ")]
    [Tooltip("¼Ì³ÐËÙ¶È")]
    [SerializeField] bool inheritSpeed;
    [Tooltip("¼Ì³Ð¹¥»÷")]
    [SerializeField] bool inheritDamage;
    [Tooltip("¼Ì³Ð×·×Ù")]
    [SerializeField] bool inheritTrackPower;

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
        if (angle > 360) angle %= 360;
        float _angle =angle/ bulletNum;
        int mid = bulletNum / 2;
        if (bulletNum % 2 == 0)
        {
            
            for (int i = 1; i <= bulletNum; i++)
            {
                GameObject bullet = ObjectPool.Instance.GetObject(bulletMode);
                bullet.transform.position = transform.position;
                
                bullet.SendMessage("SetBullet", speed);
                SetChildSpeed(bullet);
                bullet.transform.right = Quaternion.AngleAxis((i-mid-0.5f)*_angle,Vector3.forward)*transform.right;
                bullet.transform.position += bullet.transform.right.normalized * basicRadius;
            }
        }
        else
        {
            for (int i = 1; i <= bulletNum; i++)
            {
                GameObject bullet = ObjectPool.Instance.GetObject(bulletMode);
                bullet.SendMessage("SetBullet", speed);
                SetChildSpeed(bullet);
                bullet.transform.position = transform.position;
                bullet.transform.right = Quaternion.AngleAxis((i - mid-1) * _angle, Vector3.forward) * transform.right;
                bullet.transform.position+=bullet.transform.right.normalized*basicRadius;
            }
        }
        StartCoroutine(Destroy(0f));
    }
    void SetChildSpeed(GameObject childBullet)//Ð»Ð»ÍòÄÜµÄbin¸ç
    {
        Pack pack = new Pack(inheritSpeed,inheritDamage,inheritTrackPower,speed,damage,trackPower);
        childBullet.SendMessage("SetBullet", pack); 
    }
}
