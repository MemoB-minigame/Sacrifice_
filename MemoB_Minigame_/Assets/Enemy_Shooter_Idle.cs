using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter_Idle : StateMachineBehaviour
{
    Enemy_Shooter_Parameters para;
    float idleTimer;
    bool transition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para=animator.GetComponent<Enemy_Shooter_Parameters>();
        transition = true;
        idleTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer += Time.deltaTime;
        if (transition&&idleTimer >= para.idleDuration)
        {
            transition = false ;
            animator.SetTrigger("Idle>Wander");
        }
            
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
