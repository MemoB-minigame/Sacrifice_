using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] int bulletNum = 3;
    [SerializeField] float bulletAngle;
    
    
    protected override void Fire()
    {
        timer+=Time.deltaTime;  
        if (Input.GetMouseButtonDown(0) && Controller.isLife && timer >= interval&&Controller.HP-bulletNum>=0)
        {
            timer = 0;
            Controller.hurtByWeapon = true;
            Controller.HP -= bulletNum*hpCost;
            int mid = bulletNum / 2;
            float randomFireAngle;
            randomFireAngle = Random.Range(-deflectionAngle, deflectionAngle);
            direction = Quaternion.AngleAxis(randomFireAngle, Vector3.forward) * direction;
            for (int i = 1; i <= bulletNum; i++)
            {
                if (bulletNum % 2 == 0)
                {
                    //RevolverBullet bullet = Instantiate(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();
                    //bullet.SetBullet(1,bulletSpeed, Quaternion.AngleAxis(bulletAngle / 2 + (bulletAngle * (i - mid)), Vector3.forward) * direction);
                    GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
                    bullet.transform.position = muzzle.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, Quaternion.AngleAxis(bulletAngle / 2 + (bulletAngle * (i - mid)), Vector3.forward) * direction);
                }
                else
                {
                    //RevolverBullet bullet = Instantiate(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<bullet>();
                    //bullet.SetBullet(1,bulletSpeed, Quaternion.AngleAxis(bulletAngle * (i - mid), Vector3.forward) * direction);

                    GameObject bullet = ObjectPool.Instance.GetObject(bullet_Prefab);
                    bullet.transform.position = muzzle.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.GetComponent<PlayerBullet>().SetBullet(finalBulletDamage, bulletSpeed, Quaternion.AngleAxis(bulletAngle * (i - mid), Vector3.forward) * direction);
                }
            }
            RecoilForce();
        }

    }
}
