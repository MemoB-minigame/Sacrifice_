using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Alert : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;
    bool transition;

    private float alertTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        enemy = animator.gameObject;
        transform = animator.transform;
        transition = true;

        alertTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        alertTimer += Time.deltaTime;
        if (transition&&alertTimer >= para.time.alertDuration)
        {
            transition = false;
            animator.SetTrigger("AlertToShoot");
        }
        if (para.Player.transform.position.x < transform.position.x)
            transform.right = Vector3.left;
        else transform.right = Vector3.right;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
