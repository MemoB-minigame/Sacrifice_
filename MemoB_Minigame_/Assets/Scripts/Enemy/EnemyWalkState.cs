using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWalkState : StateMachineBehaviour
{
    public float speed;

    private GameObject enemy;
    private EnemyParameters parameters;
    private new Rigidbody2D rigidbody;
    private Vector3 targetPosition;
    private Vector3 direction;
    private float timer = 0;
    private float interval=3f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        enemy = animator.gameObject;
        parameters = animator.GetComponent<EnemyParameters>();
        rigidbody = animator.GetComponent<Rigidbody2D>();

        targetPosition = parameters.Player.transform.position;
        direction = targetPosition - enemy.transform.position;
        rigidbody.velocity = direction.normalized * speed;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer+=Time.deltaTime;
        if (timer < interval)
        {
            targetPosition = parameters.Player.transform.position;
            direction = targetPosition - enemy.transform.position;
            rigidbody.velocity = direction.normalized * speed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("Idle");
        }
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void onstateexit(animator animator, animatorstateinfo stateinfo, int layerindex)
    //{
    //    rigidbody.velocity = vector2.zero;
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
