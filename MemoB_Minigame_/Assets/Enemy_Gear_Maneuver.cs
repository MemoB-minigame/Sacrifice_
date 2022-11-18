using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Enemy_Gear_Maneuver : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;
    Rigidbody2D rigidbody;
    bool transition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        enemy = animator.gameObject;
        transform = animator.transform;
        transition = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(transform.position, para.Player.transform.position) > 0.1f)
        {
            rigidbody.velocity = (para.Player.transform.position - transform.position).normalized * para.speed.moveSpeed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
        if (rigidbody.velocity.x > 0)
            transform.right = Vector3.right;
        else transform.right = Vector3.left;
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
