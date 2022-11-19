using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public bool canChangeScene = false;

    public string toSceneName;

    private SceneTransition sceneTransition;

    void Start()
    {
        sceneTransition = GameObject.Find("SceneManager").GetComponent<SceneTransition>();
    }

    void Update()
    {
        if (canChangeScene && Input.GetKeyDown(KeyCode.Space))
        {
            sceneTransition.SceneChange(toSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canChangeScene = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canChangeScene = false;
        }
    }
}
