using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterB2_Parameters : Enemy_Shooter_Parameters
{
    Enemy_Shooter_Ctrl controller;

    int shootCount = 0;
    protected override void Start()
    {
        base.Start();
        muzzle = transform.Find("Muzzle").gameObject;
        controller = GetComponent<Enemy_Shooter_Ctrl>();
    }
    public override IEnumerator Shoot()
    {
        shootCount = 0;
        while (shootCount < shootRound)
        {
            //Vector3 direction = (player.transform.position - transform.position).normalized;
            //GameObject bullet = ObjectPool.Instance.GetObject(attackBulletMode);
            //bullet.transform.position = transform.position;
            //bullet.transform.rotation = Quaternion.identity;
            //bullet.transform.right = direction;
            if (controller.HP > 0)
                animator.Play("Ұ���Ļ����");
            else yield break;
            yield return new WaitForSecondsRealtime(shootDuration);
            
        }
        if(controller.HP>0)
        animator.Play("Ұ���Ļ꾯��", 0);
    }
    public void AniShootVFX()
    {
        GameObject _VFX = ObjectPool.Instance.GetObject(fireAni);
        _VFX.SendMessage("GetMuzzle", muzzle);
        _VFX.transform.position = muzzle.transform.position;    
        _VFX.transform.rotation =Quaternion.identity;
    }
    public void AniShoot()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject bullet = ObjectPool.Instance.GetObject(attackBulletMode);
        bullet.transform.position=muzzle.transform.position;    
        bullet.transform.rotation=Quaternion.identity;
        bullet.transform.right=direction;
        shootCount++;
        if(controller.HP>0)
            animator.Play("Ұ���Ļ�����ƶ�",0);
    }
}
