using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] int bulletNum = 3;
    [SerializeField] float bulletAngel;
    
    
    protected override void Fire()
    {
        timer+=Time.deltaTime;  
        if (Input.GetMouseButtonDown(0) && Controller.isLife && timer >= interval&&Controller.HP-bulletNum>=0)
        {
            timer = 0;
            Controller.HP -= bulletNum;
            int mid = bulletNum / 2;
            float randomFireAngel;
            randomFireAngel = Random.Range(-deflectionAngel, deflectionAngel);
            direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;
            for (int i = 1; i <= bulletNum; i++)
            {
                if (bulletNum % 2 == 0)
                {
                    RevolverBullet bullet = Instantiate(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();
                    bullet.SetBullet(1,bulletSpeed, Quaternion.AngleAxis(bulletAngel / 2 + (bulletAngel * (i - mid)), Vector3.forward) * direction);
                }
                else
                {
                    RevolverBullet bullet = Instantiate(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();
                    bullet.SetBullet(1,bulletSpeed, Quaternion.AngleAxis(bulletAngel * (i - mid), Vector3.forward) * direction);
                }
            }
            RecoilForce();
        }

    }
}
