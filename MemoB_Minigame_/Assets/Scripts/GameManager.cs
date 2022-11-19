using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPause = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPause)
            {
                isPause = !isPause;
                Time.timeScale = 1;
            }
            else
            {
                isPause = !isPause;
                Time.timeScale = 0;
            }
        }
    }
}
