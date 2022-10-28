using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Aggress : StateMachineBehaviour
{
    private GameObject Enemy;
    private Enemy_01_Parameters parameters;
    private Rigidbody2D rigidbody;
    private bool transition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*--------------------------------初始化--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        transition = true;
        /*--------------------------------初始化--------------------------------------*/
        rigidbody.velocity = (parameters.Player.transform.position - Enemy.transform.position).normalized * parameters.speed.aggressMoveSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(transition)rigidbody.velocity = (parameters.Player.transform.position - Enemy.transform.position).normalized * parameters.speed.aggressMoveSpeed;
        if (Vector2.Distance(parameters.Player.transform.position, Enemy.transform.position) < parameters.distance.aggressDistance && transition)
        {
            transition = false;
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("AggressToAttack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(rigidbody.velocity);
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
