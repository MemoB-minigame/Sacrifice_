using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Revolver : Gun
{
    [Header("双枪间射击间隔")]
    [SerializeField] float doubleDuration=0.2f;
    [Header("枪口二位置")]
    Transform muzzle2;
    protected override void Start()
    {
        base.Start();
        muzzle = transform.Find("Muzzle1");
        muzzle2 = transform.Find("Muzzle2");
    }
    protected override void Fire()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Controller.isLife && timer >= interval && Controller.HP - hpCost >= 0)
        {
            timer = 0;
            Controller.hurtByWeapon = true;
            Controller.HP -= hpCost;
            float randomFireAngel;
            
            randomFireAngel = Random.Range(deflectionAngel, deflectionAngel);
            direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;
            GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
            bullet.transform.position = muzzle.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, direction);
            StartCoroutine(doubleFire());
            RecoilForce();
        }
    }
    IEnumerator doubleFire()
    {
        yield return  new WaitForSeconds(doubleDuration);

        Controller.hurtByWeapon = true;
        Controller.HP -= hpCost;
        float randomFireAngel;

        randomFireAngel = Random.Range(deflectionAngel, deflectionAngel);
        direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;
        GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
        bullet.transform.position = muzzle2.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, direction);

        //RecoilForce();
    }
}
