using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy_Shooter_Parameters :MonoBehaviour
{
    
    public GameObject player;

    [Header("位置")]
    public Vector3 rebornPos;
    public LayerMask border;
    [Header("速度")]
    public float wanderSpeed;
    public float alertMoveSpeed;
    public float aggressMoveSpeed;
    public float dashSpeed;
    [Header("距离")]
    public float alertDistance;
    public float wanderRadius;
    public float wanderDistanceLowerBound;
    public float wanderDistanceUpperBound;
    public float alertRetainDistance;
    public float shootRetainDistance;
    [Header("时间")]
    public float idleDuration;
    public float wanderDuration;
    public float alertDurationLowerBound;
    public float alertDurationUpperBound;
    public int shootRound;
    public float shootDuration;
    [Header("属性")]
    public GameObject attackBulletMode;
    public GameObject deathBulletMode;
    public int Hp;
    public int regeneration;
    [Header("触发器")]
    public bool alertTrigger;

    private void Start()
    {
        player = GameObject.Find("Player");
        rebornPos = transform.position;
    }
    private void OnDrawGizmos()
    {
        if (rebornPos != Vector3.zero)
            Gizmos.DrawWireSphere(rebornPos, wanderRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderDistanceLowerBound);
        Gizmos.DrawWireSphere(transform.position, wanderDistanceUpperBound);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertDistance);
        Gizmos.color= Color.green;
        Gizmos.DrawWireSphere(transform.position, alertRetainDistance);
        
    }

    public IEnumerator Shoot()
    {
        int shootCount = 0;
        while (shootCount < shootRound)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GameObject bullet = ObjectPool.Instance.GetObject(attackBulletMode);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.transform.right = direction;
            shootCount++;

            yield return new WaitForSecondsRealtime(shootDuration);
        }
    }
}
