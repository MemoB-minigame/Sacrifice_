using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationAuto : MonoBehaviour
{
    public string toSceneName;

    private SceneTransition sceneTransition;

    void Start()
    {


        sceneTransition = GameObject.Find("SceneManager").GetComponent<SceneTransition>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneTransition.SceneChange(toSceneName);
        }
    }
}
