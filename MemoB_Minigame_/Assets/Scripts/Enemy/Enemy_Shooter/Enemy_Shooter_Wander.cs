using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy_Shooter_Wander : StateMachineBehaviour
{
    GameObject enemy;
    Enemy_Shooter_Parameters para;
    new Rigidbody2D rigidbody;
    bool transition;

    Vector3 wanderTargetPosition;
    float wanderTimer; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        para = animator.GetComponent<Enemy_Shooter_Parameters>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        transition = true;
        wanderTimer = 0;
        RaycastHit2D[] cast;
        do//生成随机游荡点
        {
            wanderTargetPosition = SpawnRandomPoint();
            Vector3 direction = (wanderTargetPosition - enemy.transform.position).normalized;
            cast = Physics2D.RaycastAll(enemy.transform.position, direction, Vector3.Distance(enemy.transform.position, wanderTargetPosition)+1.1f, (int)para.border);
            Debug.DrawRay(enemy.transform.position, direction* (Vector3.Distance(enemy.transform.position, wanderTargetPosition)+1.1f));
        } while (Vector3.Distance(para.rebornPos, wanderTargetPosition) > para.wanderRadius ||cast.Length!=0 );
        //  GameObject.Find("TestPoint").transform.position = wanderTargetPosition;
        rigidbody.velocity = (new Vector3(wanderTargetPosition.x, wanderTargetPosition.y, 0) - enemy.transform.position).normalized * para.wanderSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderTimer+=Time.deltaTime;
        if (transition)
        {
            bool timeTransition = wanderTimer >para.wanderDuration; // 如果时间到了
            bool distanceTransition = Vector2.Distance(enemy.transform.position, wanderTargetPosition) <= 0.4f; // 如果距离离游荡点够近了
            bool alertTransition = Vector3.Distance(enemy.transform.position, para.player.transform.position) < para.alertDistance && para.alertTrigger; // 如果玩家进入警戒范围了
            if (timeTransition || distanceTransition)
            {
                transition = false;
                animator.SetTrigger("Wander>Idle");
            }
            else if (alertTransition)
            {
                transition = false;
                animator.SetTrigger(">Alert");
            }
        }
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
    Vector2 SpawnRandomPoint()
    {
        Vector2 targetPosition;
        float randomDistance, randomAngle;
        randomDistance = Random.Range(para.wanderDistanceLowerBound, para.wanderDistanceUpperBound);
        randomAngle = Random.Range(-180, 180);

        targetPosition = (Quaternion.AngleAxis(randomAngle, Vector3.forward) * Vector2.up * randomDistance) + enemy.transform.position;

        return targetPosition;
    }
}
