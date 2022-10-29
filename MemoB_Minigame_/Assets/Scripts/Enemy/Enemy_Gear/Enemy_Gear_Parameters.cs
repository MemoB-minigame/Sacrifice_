using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Parameters : MonoBehaviour
{
    private void Start()
    {
        position.rebornPos = transform.position;
        Player = GameObject.Find("Player");

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
    public struct Time
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
    }
    [NonSerialized] public GameObject Player;
    public bool alertTrigger;
    [SerializeField] public Position position;
    [SerializeField] public Speed speed;
    [SerializeField] public Distance distance;
    [SerializeField] public Time time;
    [SerializeField] public Attribute attribute;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance.alertDistance);
    }
}
