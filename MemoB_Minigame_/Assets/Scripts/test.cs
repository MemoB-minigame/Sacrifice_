using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject cube;
    public Sprite fd;

    void Start()
    {
        fd = Resources.LoadAll<Sprite>("Sprites/Outside_A2_VS")[1];

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                //Instantiate<GameObject>(cube, new Vector3(i, 0, j), Quaternion.identity).GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0,256)/255f, Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f) ;
                //Instantiate<GameObject>(cube, new Vector3(i, j, 0), Quaternion.identity).GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f);
                Instantiate<GameObject>(cube, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
