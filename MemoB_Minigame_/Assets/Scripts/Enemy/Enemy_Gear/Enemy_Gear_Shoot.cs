using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Shoot : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;
    Rigidbody2D rigidbody;
    bool transition;

    private float shootTimer;
    private int shootCount;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        rigidbody=animator.GetComponent<Rigidbody2D>();
        enemy = animator.gameObject;
        transform = animator.transform;
        transition = true;

        shootCount = 0;
        shootTimer = 0;
    }

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
        if (shootCount < para.time.shootRound)
        {
            shootTimer+=Time.deltaTime;
            if (shootTimer >= para.time.shootDuration)
            {
                shootCount++;
                shootTimer = 0;
                //Instantiate(para.attribute.attackBulletMode, transform.position, Quaternion.identity);
                GameObject attack = ObjectPool.Instance.GetObject(para.attribute.attackBulletMode);
                attack.transform.position = transform.position;
                attack.transform.right = (para.Player.transform.position - animator.transform.position).normalized;
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
