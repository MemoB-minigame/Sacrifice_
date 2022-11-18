using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterMove : StateMachineBehaviour
{
    public bool facePlayer;
    [SerializeField] bool spriteReverse = false;
    protected EnemyShooterController para;
    protected GameObject enemy;
    protected new Rigidbody2D rigidbody;
    protected bool transition;

    protected float shootTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<EnemyShooterController>();
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
        
        // 是否固定面朝玩家方向
        if (facePlayer)
        {
            if (!spriteReverse)//是否翻转图片
            {
                if (para.player.transform.position.x < enemy.transform.position.x)
                    enemy.transform.right = Vector3.left;
                else enemy.transform.right = Vector3.right;
            }
            else
            {
                if (para.player.transform.position.x < enemy.transform.position.x)
                    enemy.transform.right = Vector3.right;
                else enemy.transform.right = Vector3.left;
            }
        }
        else
        {
            if (rigidbody.velocity.x >= 0)
                enemy.transform.right = Vector3.right;
            else enemy.transform.right = Vector3.left;
        }
    }
}
