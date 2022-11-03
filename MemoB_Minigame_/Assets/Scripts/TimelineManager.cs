using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    //public bool fix = false;

    public PlayableDirector playableDirector;

    //public Animator boss_Anim;
    //public RuntimeAnimatorController boss_RTAnim;
    //public Animator player_Anim;
    //public RuntimeAnimatorController player_RTAnim;

    void OnEnable()
    {
        //boss_RTAnim = boss_Anim.runtimeAnimatorController;
        //boss_Anim.runtimeAnimatorController = null;
        //player_RTAnim = player_Anim.runtimeAnimatorController;
        //player_Anim.runtimeAnimatorController = null;
    }

    private void Update()
    {
        //if (playableDirector.state != PlayState.Playing && !fix)
        //{
        //    fix = true;
        //    boss_Anim.runtimeAnimatorController = boss_RTAnim;
        //    player_Anim.runtimeAnimatorController = player_RTAnim;
        //}
    }
    //public void Set_01()
    //{
    //    boss.transform.position = new Vector3(28, -11, 0);
    //    player.GetComponent<SpriteRenderer>().flipX = true;
    //}
}
