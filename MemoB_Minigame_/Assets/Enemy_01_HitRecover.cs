using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_HitRecover : StateMachineBehaviour
{
    private float interruptTimer;
    private GameObject Enemy;
    private Enemy_01_Parameters parameters;
    private bool transition;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*--------------------------------初始化--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        transition = true;
        interruptTimer = 0;
        /*--------------------------------初始化--------------------------------------*/
        
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        interruptTimer+=Time.deltaTime;
        if (transition && interruptTimer >= parameters.time.interruptDuration)
        {
            transition = false;
            animator.SetTrigger("HitRecover");
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
