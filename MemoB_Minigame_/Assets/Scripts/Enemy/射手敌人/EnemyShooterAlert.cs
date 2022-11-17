using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAlert : StateMachineBehaviour
{
    public bool facePlayer;
    EnemyShooterController para;
    GameObject enemy;
    float alertTimer;
    bool transition;
    new Rigidbody2D rigidbody;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<EnemyShooterController>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        enemy = animator.gameObject;
        transition = true;
        alertTimer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        alertTimer += Time.deltaTime;
        if (Vector3.Distance(para.player.transform.position, enemy.transform.position) > para.alertRetainDistance + 0.08f)
        {
            rigidbody.velocity = (para.player.transform.position - enemy.transform.position).normalized * para.alertMoveSpeed;
        }
        else if (Vector3.Distance(para.player.transform.position, enemy.transform.position) < para.alertRetainDistance - 0.08f)
        {
            rigidbody.velocity = (-para.player.transform.position + enemy.transform.position).normalized * para.alertMoveSpeed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }

        // 是否固定面朝玩家方向
        if (facePlayer)
        {
            if (para.player.transform.position.x < enemy.transform.position.x)
                enemy.transform.right = Vector3.left;
            else enemy.transform.right = Vector3.right;
        }
        else
        {
            if (rigidbody.velocity.x >= 0)
                enemy.transform.right = Vector3.right;
            else enemy.transform.right = Vector3.left;
        }

        if (transition && (alertTimer > para.alertDurationUpperBound || (Vector3.Distance(para.player.transform.position, enemy.transform.position) < para.alertRetainDistance + 0.08f) && alertTimer > para.alertDurationLowerBound))
        {
            animator.SetTrigger("Alert>Shoot");
            transition = false;
        }
    }




    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.velocity = Vector3.zero;
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
