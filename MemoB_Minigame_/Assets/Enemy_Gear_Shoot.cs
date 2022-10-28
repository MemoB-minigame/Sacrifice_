using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Shoot : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;
    bool transition;

    private float shootTimer;
    private int shootCount;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        enemy = animator.gameObject;
        transform = animator.transform;
        transition = true;

        shootCount = 0;
        shootTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shootCount < para.time.shootRound)
        {
            shootTimer+=Time.deltaTime;
            if (shootTimer >= para.time.shootDuration)
            {
                shootCount++;
                shootTimer = 0;
                Instantiate(para.attribute.attackBulletMode, transform.position, Quaternion.identity);
            }
        }
        else if(transition)
        {
            transition = false;
            animator.SetTrigger("ShootToAlert");
        }
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
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
