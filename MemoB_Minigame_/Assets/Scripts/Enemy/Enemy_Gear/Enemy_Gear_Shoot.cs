using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Shoot : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;
    Rigidbody2D rigidbody;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        para.StartCoroutine(para.Shoot());
    }

    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
        
    //}

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

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
