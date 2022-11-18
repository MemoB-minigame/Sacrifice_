using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : Enemy_Shooter_Parameters
{
    int shootCount = 0;
    
    protected override void Start()
    {
        base.Start();
    }
    public override IEnumerator Shoot()
    {
        if (Hp > 0)
        {
            shootCount = 0;
            while (shootCount < shootRound)
            {
                animator.Play("Shoot");
                yield return new WaitForSecondsRealtime(shootDuration);

            }
            animator.Play("Alert");
        }
    }
    public void AniShoot()
    {
        if (Hp > 0)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GameObject bullet = ObjectPool.Instance.GetObject(attackBulletMode);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            //bullet.transform.right = direction;
            bullet.SendMessage("SetBulletD", direction);
            shootCount++;
            animator.Play("Maneuver");
        }
    }

    public void AniShootNoTransfer()
    {
        if (Hp > 0)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GameObject bullet = ObjectPool.Instance.GetObject(attackBulletMode);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            //bullet.transform.right = direction;
            bullet.SendMessage("SetBulletD", direction);
            shootCount++;
        }
    }
    
}
