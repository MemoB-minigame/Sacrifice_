using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Playables;

public class Gun : MonoBehaviour
{
    
    protected float timer=10;//计时器
    protected Vector2 direction;//发射方向
    protected PlayerController  Controller;
    protected Transform muzzle;
    protected GameObject Player;

    [SerializeField] protected int bulletDamage=1;//原本的子弹伤害
    protected int finalBulletDamage;//计算完buff后的子弹伤害
    [SerializeField]protected float bulletSpeed;//子弹速度
    [SerializeField] protected float hardRecoilForce=1;//前半段较为快速的后座力
    [SerializeField] protected float smoothRecoilForce=1;//后半段较为缓和的后坐力
    [SerializeField] protected float jump;//后坐力人物上移
    [SerializeField] protected float interval=0.384f;//发射间隔
    [SerializeField] protected int hpCost = 1;
    [SerializeField] protected float deflectionAngel = 5f;
    [SerializeField]protected GameObject bullet_Prefab;
    CinemachineImpulseSource impulse;//屏幕震动

    protected Vector2 mousePos;//鼠标位置

    //buff
    private BuffManager buffManager;
    private float damageMutiple=1;
    private float originShakeAmplitude;
    private float originShakeFrequency;

    float flipY,flipX;//枪支上下翻转
    [Header("测试选项")]//FIXME:记得去除这个
    [SerializeField] bool Fortest;
    protected virtual void Start()
    {
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        Player = GameObject.Find("Player");
        Controller=Player.GetComponent<PlayerController>();
        muzzle=transform.Find("Muzzle");

        flipX=transform.localScale.x;
        flipY = transform.localScale.y;

        impulse=GetComponent<CinemachineImpulseSource>();
        originShakeAmplitude = impulse.m_ImpulseDefinition.m_AmplitudeGain;
        originShakeFrequency = impulse.m_ImpulseDefinition.m_FrequencyGain;
    }

    protected virtual void Update()
    {
        CheckBuffs();
        Shoot();
        Fire();
    }
    protected virtual void Shoot()
    {
        
        mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos-(new Vector2(transform.position.x, transform.position.y))).normalized;

        if (Fortest||(!Controller.dialogPanelController.isSpeaking && Controller.playableDirector.state != PlayState.Playing))  //过场动画时枪不朝向鼠标
        {
            transform.right = direction;
        }

        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(flipX, flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipX, -flipY, 1);
        }
    }
    protected virtual void Fire()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Controller.isLife&&timer>=interval&&Controller.HP-hpCost>=0 && !Controller.dialogPanelController.isSpeaking && Controller.playableDirector.state != PlayState.Playing)
        {
            timer=0;
            Controller.hurtByWeapon = true;
            Controller.HP-=hpCost;
            float randomFireAngel;
            randomFireAngel = Random.Range(deflectionAngel, deflectionAngel);
            direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;
            GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
            bullet.transform.position = muzzle.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, direction);

            RecoilForce();
        }
    }
    protected virtual void CheckBuffs()
    {
        if (buffManager.ifLastBulletKills && buffManager.buffs[3])
        {
            buffManager.ifLastBulletKills = false;
            finalBulletDamage = bulletDamage * 2;
            impulse.m_ImpulseDefinition.m_AmplitudeGain = originShakeAmplitude*1.5f;
            impulse.m_ImpulseDefinition.m_FrequencyGain = originShakeFrequency*1.5f;
        }
    }
    protected virtual void RecoilForce()
    {
        finalBulletDamage = bulletDamage;

        impulse.GenerateImpulse();
        impulse.m_ImpulseDefinition.m_AmplitudeGain = originShakeAmplitude;
        impulse.m_ImpulseDefinition.m_FrequencyGain = originShakeFrequency;
        StartCoroutine(Recoil(hardRecoilForce, smoothRecoilForce));
    }
    IEnumerator Recoil(float hard,float smooth)
    {
        for(int i=1;i<=2; i++)
        {
            Player.transform.Translate(-direction / 5*hard);
            Player.transform.Translate(Vector3.up/20*jump);
            yield return new WaitForFixedUpdate();
        }
        for (int i = 1; i <= 6; i++)
        {
            Player.transform.Translate(-direction / 20*smooth);
            Player.transform.Translate(Vector3.down / 60 * jump);
            yield return new WaitForFixedUpdate();
        }
    }
}
