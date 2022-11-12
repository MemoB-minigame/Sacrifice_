using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_TrackBullet : Enemy_Bullet_Basic
{
    [Header("¼ä¾à")]
    [SerializeField] float deltaDistance;
    [Header("×Óµ¯ÉèÖÃ")]
    [SerializeField] GameObject[] trackBulletModes;

    int index;
    [SerializeField]private float distanceRecorder;
    Vector3 lastPos;
    protected override void OnEnable()
    {
        base.OnEnable();
        distanceRecorder = 0;
        index = 0;
        lastPos = new Vector3(-11, -.4f, -.514f);
    }
    protected override void Update()
    {
        base.Update();
        if (lastPos != new Vector3(-11, -.4f, -.514f)) 
            distanceRecorder += Vector3.Distance(transform.position, lastPos);
        lastPos = transform.position;
        if (distanceRecorder >= deltaDistance&&index<trackBulletModes.Length)
        {
            distanceRecorder = 0;
            Generate();
        }
        
    }
    void Generate()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(trackBulletModes[index++]);
        bullet.transform.position=transform.position;
        bullet.transform.right = transform.right;
        var bulletCtrl = bullet.GetComponent<Enemy_Bullet_Basic>();
        if(bulletCtrl==null)bulletCtrl = bullet.AddComponent<Enemy_Bullet_Queue>();
        else if (bulletCtrl == null) bulletCtrl = bullet.AddComponent<Enemy_Bullet_TrackBullet>();
        else if (bulletCtrl == null) bulletCtrl = bullet.AddComponent<Enemy_Bullet_Circle>();
        else if (bulletCtrl == null) bulletCtrl = bullet.AddComponent<Enemy_Bullet_Inferior>();
        bulletCtrl.SetBullet(damage, 0, trackPower);
    }

}
