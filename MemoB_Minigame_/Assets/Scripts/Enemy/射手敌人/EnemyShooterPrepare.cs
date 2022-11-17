using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterPrepare : StateMachineBehaviour
{
    protected EnemyShooterController para;
    protected GameObject enemy;
    protected new Rigidbody2D rigidbody;
    protected bool transition;

    protected float shootTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<EnemyShooterController>();
        para.StartCoroutine(para.Shoot());
        shootTimer = 0;
    }

}
