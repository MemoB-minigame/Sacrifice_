using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Attack : StateMachineBehaviour
{
    private GameObject Enemy;
    private float dashTimer = 0;
    private Enemy_01_Parameters parameters;
    private bool transition;
    private Rigidbody2D rigidbody;

    private Vector3 targetPosition;
    private Vector3 direction;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("EnterAttack");
        /*--------------------------------初始化--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        rigidbody=animator.gameObject.GetComponent<Rigidbody2D>();
        transition = true;
        dashTimer = 0;
        /*--------------------------------初始化--------------------------------------*/
        targetPosition = Enemy.transform.position + parameters.distance.dashDistanceMultiplier * (parameters.Player.transform.position - Enemy.transform.position);
        targetPosition.z = 0;
        direction = (targetPosition - Enemy.transform.position).normalized;
        rigidbody.velocity = direction * parameters.speed.dashSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;
        
        bool timeTransition = dashTimer >= parameters.time.dashDuration;
        bool posTransition = Vector3.Distance(Enemy.transform.position, targetPosition) < 0.4f;
        if (transition&&(timeTransition||posTransition))
        {
            transition = false;
            
            animator.SetTrigger("AttackBackToAlert");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.velocity = Vector2.zero;
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
