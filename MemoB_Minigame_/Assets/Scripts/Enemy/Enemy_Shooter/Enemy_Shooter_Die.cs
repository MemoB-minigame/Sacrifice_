using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter_Die : StateMachineBehaviour
{
    Enemy_Shooter_Parameters para;
    Rigidbody2D rigidbody;
    Transform transform;
    bool first;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody = animator.GetComponent<Rigidbody2D>();
        transform = animator.transform;
        para=animator.GetComponent<Enemy_Shooter_Parameters>();
        first=true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.velocity = Vector2.zero;
        if (stateInfo.normalizedTime >= 0.95&&first)
        {
            first=false;
            if(para.deathBulletMode!=null)
                Instantiate(para.deathBulletMode, transform.position, Quaternion.identity);

            Instantiate(para.regenerationPrefab, transform.position, Quaternion.identity).SendMessage("SetRegeneration", para.regeneration); 
            Destroy(animator.gameObject);
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
