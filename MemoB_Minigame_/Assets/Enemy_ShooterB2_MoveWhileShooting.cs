using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterB2_MoveWhileShooting : StateMachineBehaviour
{
    protected Enemy_ShooterB2_Parameters para;
    protected GameObject enemy;
    protected new Rigidbody2D rigidbody;
    protected bool transition;

    protected float shootTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_ShooterB2_Parameters>();
        enemy = animator.gameObject;
        rigidbody = animator.GetComponent<Rigidbody2D>();
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

        if (rigidbody.velocity.x >= 0)
            enemy.transform.right = Vector3.right;
        else enemy.transform.right = Vector3.left;

    }
}
