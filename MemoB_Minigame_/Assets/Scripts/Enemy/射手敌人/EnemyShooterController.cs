using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : Enemy_Shooter_Parameters
{
    int shootCount = 0;
    Enemy_Shooter_Ctrl controller;
    
    protected override void Start()
    {
        controller = GetComponent<Enemy_Shooter_Ctrl>();
        base.Start();
    }
    public override IEnumerator Shoot()
    {
        if (controller.HP > 0)
        {
            shootCount = 0;
            while (shootCount < shootRound)
            {
                if (controller.HP > 0)
                    animator.Play("Shoot");
                else yield break;
                yield return new WaitForSecondsRealtime(shootDuration);

            }
            if(controller.HP>0)
                animator.Play("Alert");
        }
    }
    public void AniShoot()
    {
        if (controller.HP>0)
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
        if (controller.HP > 0)
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
