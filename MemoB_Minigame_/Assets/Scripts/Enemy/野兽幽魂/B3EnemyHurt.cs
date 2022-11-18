using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3EnemyHurt : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    Color originColor;
    bool isPlayingHurtAni=false;

   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        originColor=spriteRenderer.color;
    }
    public void HurtAni()
    {
        if (!isPlayingHurtAni)
        {
            isPlayingHurtAni = true;
            StartCoroutine(PlayHurtAni());
        }
    }
    IEnumerator PlayHurtAni()
    {
        bool ifRedColor=false;
        for(int i = 0; i < 6; i++)
        {
            if (ifRedColor)
            {
                ifRedColor=false;
                spriteRenderer.color = Color.red;
            }
            else
            {
                ifRedColor = true;
                spriteRenderer.color = originColor;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
        spriteRenderer.color = originColor;
        isPlayingHurtAni = false;
    }
}
