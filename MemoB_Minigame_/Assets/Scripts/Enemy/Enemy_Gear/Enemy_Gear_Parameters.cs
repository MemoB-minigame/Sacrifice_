using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Parameters : MonoBehaviour
{
    Animator animator;
    Enemy_Gear_Ctrl controller;
    Transform muzzle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<Enemy_Gear_Ctrl>();
        position.rebornPos = transform.position;
        Player = GameObject.Find("Player");
        muzzle = transform.Find("Muzzle");

    }
    private void Update()
    {
        
    }
    [System.Serializable]
    public struct Position
    {
        public Vector3 rebornPos;
    }
    [System.Serializable]
    public struct Speed
    {
        public float moveSpeed;
    }
    [System.Serializable]
    public struct Distance
    {
        public float alertDistance;
    }
    [System.Serializable]
    public struct Timee
    {
        public float alertDuration;
        public int shootRound;
        public float shootDuration;
    }
    [System.Serializable]
    public struct Attribute
    {
        public GameObject attackBulletMode;
        public GameObject deathBulletMode;
        public int Hp;
        public int regeneration;
        public GameObject regenerationPrefab;
    }
    [NonSerialized] public GameObject Player;
    public bool alertTrigger;
    [SerializeField] public Position position;
    [SerializeField] public Speed speed;
    [SerializeField] public Distance distance;
    [SerializeField] public Timee time;
    [SerializeField] public Attribute attribute;


    public int shootCount = 0;
    public IEnumerator Shoot()
    {
        float shootTimer=0;
       shootCount = 0;
        while (shootCount < time.shootRound)
        {
            if (shootTimer > time.shootDuration&&controller.Hp>0)
            {
                shootTimer = 0;
                animator.Play("Shoot");
            }
            else if (controller.Hp <= 0)
            {
                yield break;
            }
            shootTimer+=Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        if(controller.Hp>0)
            animator.Play("Alert");
    }
    public void AniShoot()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        GameObject bullet = ObjectPool.Instance.GetObject(attribute.attackBulletMode);
        bullet.transform.position = muzzle.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.transform.right = direction;
        shootCount++;
        if (controller.Hp > 0)
            animator.Play("Maneuver");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance.alertDistance);
    }
}
