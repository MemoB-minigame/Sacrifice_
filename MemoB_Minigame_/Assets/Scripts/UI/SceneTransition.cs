using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //public string toSceneName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SceneChange(string toSceneName)
    {
        SceneManager.LoadScene(toSceneName);
    }
}
