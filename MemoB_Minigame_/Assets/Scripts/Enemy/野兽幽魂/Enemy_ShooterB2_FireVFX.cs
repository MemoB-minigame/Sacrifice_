using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterB2_FireVFX : MonoBehaviour
{
    GameObject muzzle;
    private void Update()
    {
        FollowMuzzle();
    }
    public void GetMuzzle(GameObject _muzzle)
    {
        muzzle = _muzzle;
    }
    private void FollowMuzzle()
    {
        if(muzzle!=null)
            transform.position=muzzle.transform.position;
    }
    private void PushObject()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
