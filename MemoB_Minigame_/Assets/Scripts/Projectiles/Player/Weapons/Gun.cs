using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected float timer=10;//��ʱ��
    protected Vector2 direction;//���䷽��
    protected PlayerController  Controller;
    protected Transform muzzle;
    [SerializeField]protected GameObject Player;
    [SerializeField]protected GameObject bullet_Prefab;
    [SerializeField]protected float bulletSpeed;//�ӵ��ٶ�
    [SerializeField] protected float hardRecoilForce=1;//ǰ��ν�Ϊ���ٵĺ�����
    [SerializeField] protected float smoothRecoilForce=1;//���ν�Ϊ���͵ĺ�����
    [SerializeField] protected float jump;//��������������
    [SerializeField] protected float interval=0.384f;//������
    [SerializeField] protected int hpCost = 1;
    [SerializeField] protected float deflectionAngel = 5f;
    CinemachineImpulseSource impulse;//��Ļ��

    protected Vector2 mousePos;//���λ��
    

    float flipY,flipX;//ǹ֧���·�ת
    protected virtual void Start()
    {
        flipX=transform.localScale.x;
        flipY = transform.localScale.y;
        Controller=Player.GetComponent<PlayerController>();
        muzzle=transform.Find("Muzzle");
        impulse=GetComponent<CinemachineImpulseSource>();   
    }

    protected virtual void Update()
    {
        Shoot();
        Fire();
    }
    protected virtual void Shoot()
    {
        
        mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos-(new Vector2(transform.position.x, transform.position.y))).normalized;
        transform.right = direction;

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
        if (Input.GetMouseButtonDown(0) && Controller.isLife&&timer>=interval&&Controller.HP-hpCost>=0)
        {
            timer=0;
            Controller.HP-=hpCost;
            float randomFireAngel;
            randomFireAngel = Random.Range(deflectionAngel, deflectionAngel);
            direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;
            RevolverBullet revolverBullet = Instantiate<GameObject>(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();
            revolverBullet.SetBullet(hpCost,bulletSpeed, direction);
            
            RecoilForce();
        }
    }
    protected virtual void RecoilForce()
    {
            impulse.GenerateImpulse();
            StartCoroutine(Recoil(hardRecoilForce,smoothRecoilForce));
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
