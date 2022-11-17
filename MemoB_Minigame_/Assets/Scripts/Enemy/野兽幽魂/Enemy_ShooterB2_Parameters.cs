using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterB2_Parameters : Enemy_Shooter_Parameters
{
    int shootCount = 0;
    protected override void Start()
    {
        base.Start();
        muzzle = transform.Find("Muzzle").gameObject;
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
            animator.Play("Ò°ÊÞÓÄ»êÉä»÷");
            yield return new WaitForSecondsRealtime(shootDuration);
            
        }
        animator.Play("Ò°ÊÞÓÄ»ê¾¯½ä", 0);
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
        animator.Play("Ò°ÊÞÓÄ»êÉä»÷ÒÆ¶¯",0);
    }
}
