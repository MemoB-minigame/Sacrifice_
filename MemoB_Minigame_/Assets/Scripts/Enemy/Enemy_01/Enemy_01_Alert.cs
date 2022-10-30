using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Alert : StateMachineBehaviour
{
    private GameObject Enemy;
    private float alertTimer = 0;
    private Enemy_01_Parameters parameters;
    private Rigidbody2D rigidbody;
    private bool transition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*--------------------------------初始化--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        rigidbody= animator.gameObject.GetComponent<Rigidbody2D>(); 
        transition = true;
        alertTimer = 0;
        /*--------------------------------初始化--------------------------------------*/
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        alertTimer+=Time.deltaTime;
        if (alertTimer < parameters.time.alertDuration)
        {
            if (Vector3.Distance(parameters.Player.transform.position, Enemy.transform.position) > parameters.distance.retainDistance+0.5f)
            {
                rigidbody.velocity= (parameters.Player.transform.position-Enemy.transform.position).normalized*parameters.speed.alertMoveSpeed;
            }
            else if(Vector3.Distance(parameters.Player.transform.position, Enemy.transform.position) < parameters.distance.retainDistance - 0.5f)
            {
                rigidbody.velocity = (-parameters.Player.transform.position + Enemy.transform.position).normalized * parameters.speed.alertMoveSpeed;
            }
            else 
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
        else if(transition)
        {
            transition = false;
            animator.SetTrigger("AlertToAggress");
        }
    }

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
