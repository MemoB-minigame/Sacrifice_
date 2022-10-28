using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01_Idle : StateMachineBehaviour
{
    private GameObject Enemy;
    private float idleTimer = 0;
    private Enemy_01_Parameters parameters;
    private bool transition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /*--------------------------------��ʼ��--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        transition = true;
        /*--------------------------------��ʼ��--------------------------------------*/
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > parameters.time.idleDuration && transition)
        {
            animator.SetTrigger("IdleToWander");
            transition = false;//��ֹ�����������bug
        }
        if (transition && parameters.alertTrigger && Vector3.Distance(parameters.Player.transform.position, Enemy.transform.position) < parameters.distance.alertDistance)
        {
            transition = false;
            animator.SetTrigger("ToAlert");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer = 0;
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
