using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Revolver : Gun
{
    private Animator aniMuzzle2;
    private Animator muzzleSmoke2;
    [Header("双枪间射击间隔")]
    [SerializeField] float doubleDuration=0.2f;
    [Header("枪口二位置")]
    Transform muzzle2;
    protected override void Start()
    {
        base.Start();
        muzzle = transform.Find("Muzzle1");
        muzzle2 = transform.Find("Muzzle2");
        aniMuzzle2 = transform.Find("AniMuzzle2").GetComponent<Animator>();
        muzzleSmoke2 = transform.Find("MuzzleSmoke2").GetComponent<Animator>();
    }
    protected override void Fire()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Controller.isLife && timer >= interval && Controller.HP - hpCost*2 >= 0)
        {
            timer = 0;
            Controller.hurtByWeapon = true;
            Controller.HP -= hpCost;
            float randomFireAngle;
            
            randomFireAngle = Random.Range(deflectionAngle, deflectionAngle);
            direction = Quaternion.AngleAxis(randomFireAngle, Vector3.forward) * direction;
            GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
            bullet.transform.position = muzzle.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, direction);
            StartCoroutine(PlayFireAni());
            StartCoroutine(doubleFire());
            RecoilForce();
            fireSoundEffect.Play();
        }
    }
    IEnumerator doubleFire()
    {
        yield return  new WaitForSeconds(doubleDuration);

        Controller.hurtByWeapon = true;
        Controller.HP -= hpCost;
        float randomFireAngle;

        randomFireAngle = Random.Range(deflectionAngle, deflectionAngle);
        direction = Quaternion.AngleAxis(randomFireAngle, Vector3.forward) * direction;
        GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
        bullet.transform.position = muzzle2.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, direction);
        StartCoroutine(PlayFireAni2());

        //RecoilForce();
    }
    private IEnumerator PlayFireAni2()
    {
        aniMuzzle2.gameObject.SetActive(true);
        while (aniMuzzle2.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.95f)
        {
            yield return new WaitForFixedUpdate();

        }
        
        aniMuzzle2.gameObject.SetActive(false);
        muzzleSmoke2.gameObject.SetActive(true);
        muzzleSmoke2.Play("Smoke");
    }
}
