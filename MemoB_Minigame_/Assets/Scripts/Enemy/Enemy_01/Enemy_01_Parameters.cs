using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Parameters : MonoBehaviour
{
    
    [Header("Ç½±Ú")]
    public LayerMask border;
    private void Start()
    {
        position.rebornPos = transform.position;
        Player = GameObject.Find("Player");
    }
    #region 
    [System.Serializable]
    public struct Position
    {
        public Vector3 rebornPos;
    }
    [System.Serializable]
    public struct Speed
    {
        public float wanderSpeed;
        public float alertMoveSpeed;
        public float aggressMoveSpeed;
        public float dashSpeed;
    }
    [System.Serializable]
    public struct Distance
    {
        public float wanderRadius;
        public float wanderDistanceLowerBound;
        public float wanderDistanceUpperBound;
        public float alertDistance;
        public float retainDistance;
        public float aggressDistance;
        public float dashDistanceMultiplier;
    }
    [System.Serializable]
    public struct Time
    {
        public float idleDuration;
        public float wanderDuration;
        public float dashDuration;
        public float alertDuration;
        public float interruptDuration;
    }
    [System.Serializable]
    public struct Attribute
    {
        public float attackDamage;
        public int Hp;
        public float regeneration;
        public GameObject regenerationPrefab;
    }
    [NonSerialized]public GameObject Player;
    public bool alertTrigger;
    [SerializeField] public Position position;
    [SerializeField] public Speed speed;
    [SerializeField] public Distance distance;
    [SerializeField] public Time time;
    [SerializeField] public Attribute attribute;
    #endregion//
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position.rebornPos, distance.wanderRadius);//³öÉúµã·¶Î§
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance.wanderDistanceUpperBound);
        Gizmos.DrawWireSphere(transform.position, distance.wanderDistanceLowerBound);//ÏÐ¹ä·¶Î§

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance.alertDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance.aggressDistance);


    }

}
