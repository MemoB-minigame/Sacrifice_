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
    private Vector3 wanderTargetPosition;
    RaycastHit2D[] cast;
    Vector3 direction;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*--------------------------------初始化--------------------------------------*/
        Enemy = animator.gameObject;
        parameters = animator.gameObject.GetComponent<Enemy_01_Parameters>();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();    
        transition = true;
        wanderTimer = 0;
        /*--------------------------------初始化--------------------------------------*/
        
        
        do//生成随机游荡点
        {
            wanderTargetPosition = SpawnRandomPoint();
            direction = (wanderTargetPosition - Enemy.transform.position).normalized;
            cast = Physics2D.RaycastAll(Enemy.transform.position, direction, Vector3.Distance(Enemy.transform.position, wanderTargetPosition)+1.1f, (int)parameters.border);

        } while(Vector2.Distance(parameters.position.rebornPos,wanderTargetPosition)>parameters.distance.wanderRadius || cast.Length != 0);//如果生成的目标点离出生点太远
        GameObject.Find("TestPoint").transform.position = wanderTargetPosition;
        rigidbody.velocity = (new Vector3(wanderTargetPosition.x, wanderTargetPosition.y, 0) - Enemy.transform.position).normalized * parameters.speed.wanderSpeed;//设置走向游荡点的速度(如果用moveforward可能穿墙)
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderTimer+=Time.deltaTime;
        Debug.DrawRay(Enemy.transform.position, direction * (Vector3.Distance(Enemy.transform.position, wanderTargetPosition) +1.1f));

        if (transition)//如果还没发生过状态转换
        {
            bool timeTransition = wanderTimer > parameters.time.wanderDuration;//如果时间到了
            bool distanceTransition = Vector2.Distance(Enemy.transform.position, wanderTargetPosition) <= 0.05f;//如果距离离游荡点够近了
            bool alertTransition = Vector2.Distance(parameters.Player.transform.position, Enemy.transform.position) < parameters.distance.alertDistance  &&parameters.alertTrigger;//如果玩家进入警戒范围了
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
                animator.SetTrigger("ToAlert");
            }
        }

    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
    Vector2 SpawnRandomPoint()
    {
        Vector2 targetPosition;
        float randomDistance, randomAngle;
        randomDistance = Random.Range(parameters.distance.wanderDistanceLowerBound, parameters.distance.wanderDistanceUpperBound);
        randomAngle = Random.Range(-180, 180);

        targetPosition =  (Quaternion.AngleAxis(randomAngle, Vector3.forward)*Vector2.up * randomDistance)+Enemy.transform.position;

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
