using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BulletExplosion_Enemy : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo aniInfo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        aniInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (aniInfo.normalizedTime >= 0.95f)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
