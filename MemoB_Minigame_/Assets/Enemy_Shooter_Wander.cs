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

    Vector2 wanderTargetPosition;
    float wanderTimer; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        para = animator.GetComponent<Enemy_Shooter_Parameters>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        transition = true;
        wanderTimer = 0;

        do//��������ε���
        {
            wanderTargetPosition = SpawnRandomPoint();
        } while (Vector2.Distance(para.rebornPos, wanderTargetPosition) > para.wanderRadius);
        GameObject.Find("TestPoint").transform.position = wanderTargetPosition;
        rigidbody.velocity = (new Vector3(wanderTargetPosition.x, wanderTargetPosition.y, 0) - enemy.transform.position).normalized * para.wanderSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderTimer+=Time.deltaTime;
        if (transition)
        {
            bool timeTransition = wanderTimer >para.wanderDuration;//���ʱ�䵽��
            bool distanceTransition = Vector2.Distance(enemy.transform.position, wanderTargetPosition) <= 0.4f;//����������ε��㹻����
            bool alertTransition =Vector3.Distance(enemy.transform.position,para.player.transform.position)<para.alertDurationUpperBound&&para.alertTrigger;//�����ҽ��뾯�䷶Χ��
            if (timeTransition || distanceTransition)
            {
                transition = false;
                animator.SetTrigger("Wander>Idle");
            }
            else if (alertTransition)
            {
                transition = false;
                animator.SetTrigger("Wander>Alert");
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
        float randomDistance, randomAngel;
        randomDistance = Random.Range(para.wanderDistanceLowerBound, para.wanderDistanceUpperBound);
        randomAngel = Random.Range(-180, 180);

        targetPosition = (Quaternion.AngleAxis(randomAngel, Vector3.forward) * Vector2.up * randomDistance) + enemy.transform.position;

        return targetPosition;
    }
}
