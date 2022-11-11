using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter_Shoot : StateMachineBehaviour
{
    Enemy_Shooter_Parameters para;
    GameObject enemy;
    new Rigidbody2D rigidbody;
    bool transition;

    float shootTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Shooter_Parameters>();
        enemy = animator.gameObject;
        rigidbody = animator.GetComponent<Rigidbody2D>();   
        transition = true;
        para.StartCoroutine(para.Shoot());
        shootTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shootTimer += Time.deltaTime;
        if (Vector3.Distance(para.player.transform.position, enemy.transform.position) > para.shootRetainDistance + 0.08f)
        {
            rigidbody.velocity = (para.player.transform.position - enemy.transform.position).normalized * para.shootMoveSpeed;
        }
        else if (Vector3.Distance(para.player.transform.position, enemy.transform.position) < para.shootRetainDistance - 0.08f)
        {
            rigidbody.velocity = (-para.player.transform.position + enemy.transform.position).normalized * para.shootMoveSpeed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }

        if (transition && shootTimer > (para.shootRound - 1) * para.shootDuration + 0.5f)
        {
            transition = false;
            animator.SetTrigger("Shoot>Alert");
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
