using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePauseSetManager_L0_S1 : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    void Start()
    {
        
    }


    public void Set_01()
    {
        boss.transform.position = new Vector3(28, -11, 0);
        player.GetComponent<SpriteRenderer>().flipX = true;
    }
}
