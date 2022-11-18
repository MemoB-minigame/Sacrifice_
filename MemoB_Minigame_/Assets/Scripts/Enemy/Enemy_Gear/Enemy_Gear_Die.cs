using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gear_Die : StateMachineBehaviour
{
    GameObject enemy;
    Transform transform;
    Enemy_Gear_Parameters para;

    bool first;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        para = animator.GetComponent<Enemy_Gear_Parameters>();
        enemy = animator.gameObject;
        transform = animator.transform;
        first = true;
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f&&first)
        {
            first = false;
            if(para.attribute.deathBulletMode!=null)//Éú³ÉËÀÍöµ¯Ä»
                Instantiate(para.attribute.deathBulletMode, transform.position, Quaternion.identity);

            Instantiate(para.attribute.regenerationPrefab, transform.position, Quaternion.identity).SendMessage("SetRegeneration", para.attribute.regeneration);
            Destroy(enemy.gameObject);
        }
    }
}
