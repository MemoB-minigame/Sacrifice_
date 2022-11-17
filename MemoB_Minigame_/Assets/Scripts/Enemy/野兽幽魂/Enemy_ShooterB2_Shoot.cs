using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterB2_Shoot : StateMachineBehaviour
{
    protected Enemy_ShooterB2_Parameters para;
    protected GameObject enemy;
    protected new Rigidbody2D rigidbody;
    protected bool transition;

    protected float shootTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_ShooterB2_Parameters>();
        para.StartCoroutine(para.Shoot());
        shootTimer = 0;
    }

}
