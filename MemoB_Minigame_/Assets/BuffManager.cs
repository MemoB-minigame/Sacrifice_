using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    PlayerController controller;
    public List<bool> buffs=new List<bool>();

    //×Óµ¯ÊôÐÔ
    private float damageMutiple=1f;
    private float bulletScaleX, bulletScaleY;

    [NonSerialized]public float bulletScaleMutiple=2;
    [NonSerialized]public bool ifLastBulletKills;


    private void Awake()
    {
        bulletScaleMutiple = 2;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        for (int i = 1; i <= 4; i++)
            buffs.Add(false);
    }

    private void Update()
    {
        CheckPlayerHp();
    }

    private void CheckPlayerHp()
    {
        if (controller.HP <= 20)
            buffs[1]=true;
        if(controller.HP<=16)
            buffs[2]=true;
        if(controller.HP<=22)
            buffs[3]=true;
    }
    public void BulletKills()//»÷É±µÐÈËºó×Óµ¯ÉËº¦·­±¶
    {
        ifLastBulletKills=true; 
    }
}
