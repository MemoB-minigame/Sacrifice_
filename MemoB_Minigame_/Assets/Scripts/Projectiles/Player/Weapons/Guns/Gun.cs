using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Playables;

public class Gun : MonoBehaviour
{
    
    protected float timer=10;//��ʱ��
    protected Vector2 direction;//���䷽��
    protected PlayerController  Controller;
    protected Transform muzzle;
    protected GameObject Player;

    [SerializeField] protected int bulletDamage=1;//ԭ�����ӵ��˺�
    protected int finalBulletDamage;//������buff����ӵ��˺�
    [SerializeField]protected float bulletSpeed;//�ӵ��ٶ�
    [SerializeField] protected float hardRecoilForce=1;//ǰ��ν�Ϊ���ٵĺ�����
    [SerializeField] protected float smoothRecoilForce=1;//���ν�Ϊ���͵ĺ�����
    [SerializeField] protected float jump;//��������������
    [SerializeField] protected float interval=0.384f;//������
    [SerializeField] protected int hpCost = 1;
    [SerializeField] protected float deflectionAngel = 5f;
    [SerializeField]protected GameObject bullet_Prefab;
    CinemachineImpulseSource impulse;//��Ļ��

    protected Vector2 mousePos;//���λ��

    //buff
    private BuffManager buffManager;
    private float damageMutiple=1;
    private float originShakeAmplitude;
    private float originShakeFrequency;

    float flipY,flipX;//ǹ֧���·�ת
    [Header("����ѡ��")]//FIXME:�ǵ�ȥ�����
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

        if (Fortest||(!Controller.dialogPanelController.isSpeaking && Controller.playableDirector.state != PlayState.Playing))  //��������ʱǹ���������
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
