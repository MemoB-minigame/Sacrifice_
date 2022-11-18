using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffManager : MonoBehaviour
{
    PlayerController controller;
    public List<bool> buffs=new List<bool>();

    //�ӵ�����
    [Header("Buff��������")]
    [Tooltip("��ɱ���˺��ǿ�������˺�����")]
    public int damageMutiple=1;
    [Tooltip("��ɱ���˺��ǿ��������Ļ���������")]
    public float amplitudeGainMutiple = 1.5f;
    [Tooltip("��ɱ���˺��ǿ��������Ļ��Ƶ�ʱ���")]
    public float frequencyGainMutiple=1.5f;
    [Tooltip("buff2�ӵ��Ŵ�ı���")]
    public float bulletScaleMutiple=2;

    [NonSerialized]public bool ifLastBulletKills;

    //UI
    private Image buff_0;
    private Image buff_1;
    private Image buff_2;

    private void Awake()
    {
        buff_0 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_0_Image").GetComponent<Image>();
        buff_1 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_1_Image").GetComponent<Image>();
        buff_2 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_2_Image").GetComponent<Image>();
        buff_0.enabled = false;
        buff_1.enabled = false;
        buff_2.enabled = false;


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
        {
            buffs[1] = true;
            buff_0.enabled = true;
        }
        else
        { 
            buffs[1] = false;
            buff_0.enabled = false;
        }

        if (controller.HP <= 16)
        {
            buffs[2] = true;
            buff_1.enabled = true;
        }
        else
        {
            buffs[2] = false;
            buff_1.enabled = false;
        }

        if (controller.HP <= 12)
        {
            buffs[3] = true;
            buff_2.enabled = true;
        }
        else
        { 
            buffs[3] = false;
            buff_2.enabled = false;
        }
    }
    public void BulletKills()//��ɱ���˺��ӵ��˺�����
    {
        ifLastBulletKills=true; 
    }
}
