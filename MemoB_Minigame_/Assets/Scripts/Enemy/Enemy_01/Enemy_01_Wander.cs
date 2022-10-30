using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Wander : StateMachineBehaviour
{
    private GameObject Enemy;
    private Enemy_01_Parameters parameters;
    private Rigidbody2D rigidbody;
    private bool transition;

    private float wanderTimer;
    private Vector2 wanderTargetPosition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*--------------------------------��ʼ��--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();    
        transition = true;
        wanderTimer = 0;
        /*--------------------------------��ʼ��--------------------------------------*/
        
        do//��������ε���
        {
            wanderTargetPosition = SpawnRandomPoint();
            GameObject.Find("TestPoint").transform.position = wanderTargetPosition;
        } while (Vector2.Distance(parameters.position.rebornPos,wanderTargetPosition)>parameters.distance.wanderRadius);//������ɵ�Ŀ����������̫Զ

        rigidbody.velocity = (new Vector3(wanderTargetPosition.x, wanderTargetPosition.y, 0) - Enemy.transform.position).normalized * parameters.speed.wanderSpeed;//���������ε�����ٶ�(�����moveforward���ܴ�ǽ)
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderTimer+=Time.deltaTime;

        if (transition)//�����û������״̬ת��
        {
            bool timeTransition = wanderTimer > parameters.time.wanderDuration;//���ʱ�䵽��
            bool distanceTransition = Vector2.Distance(Enemy.transform.position, wanderTargetPosition) <= 0.05f;//����������ε��㹻����
            bool alertTransition = Vector2.Distance(parameters.Player.transform.position, Enemy.transform.position) < parameters.distance.alertDistance  &&parameters.alertTrigger;//�����ҽ��뾯�䷶Χ��
            if (timeTransition||distanceTransition)
            {
                transition = false;
                rigidbody.velocity = Vector3.zero;
                animator.SetTrigger("WanderBackToIdle");
            }
            else if (alertTransition)
            {
                transition = false;
                rigidbody.velocity = Vector3.zero;
                animator.SetTrigger("WanderBackToIdle");
            }
        }

    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
    Vector2 SpawnRandomPoint()
    {
        Vector2 targetPosition;
        float randomDistance, randomAngel;
        randomDistance = Random.Range(parameters.distance.wanderDistanceLowerBound, parameters.distance.wanderDistanceUpperBound);
        randomAngel = Random.Range(-180, 180);

        targetPosition =  (Quaternion.AngleAxis(randomAngel, Vector3.forward)*Vector2.up * randomDistance)+Enemy.transform.position;

        return targetPosition;
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
