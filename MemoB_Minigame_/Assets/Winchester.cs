using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winchester : Gun
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject post;
    [SerializeField] float bigSize=2f;
    
    CinemachineTransposer transposer;
    GameObject bigPoint;
    protected override void Start()
    {
        base.Start();
        transposer = virtualCamera.GetComponent<CinemachineTransposer>();
        bigPoint = transform.Find("BigPoint").gameObject;
    }
    protected override void Update()
    {
        base.Update();
        Aim();
    }
    void Aim()
    {
        //if (Input.GetMouseButtonDown(1))
        if (Input.GetKeyDown(KeyCode.J))
        {
            //bigPoint.transform.position = post.transform.position;
            bigPoint.transform.position = new Vector3(direction.x,direction.y,0)*6.5f + Player.transform.position;
            virtualCamera.Follow = bigPoint.transform;
            //virtualCamera.m_Lens.OrthographicSize = 10.5f/bigSize;
            StartCoroutine(BecomeBigSize(10));
            post.GetComponent<Sight>().sensitivity = 1/bigSize;
            
            Debug.Log("Post");
        }
        //if (Input.GetMouseButtonUp(1))
        if (Input.GetKeyUp(KeyCode.J))
        {
            virtualCamera.Follow = Player.transform;
            post.GetComponent<Sight>().sensitivity = 1;
            StartCoroutine(ReturnSmallSize(10));
            Debug.Log("Player");
        }
    }
    IEnumerator BecomeBigSize(int damping)
    {
        for (int i = 1; i <= damping; i++)
        {
            virtualCamera.m_Lens.OrthographicSize += (10.5f / bigSize-10.5f) / damping;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ReturnSmallSize(int damping)
    {
        for(int i = 1; i <= damping; i++)
        {
            virtualCamera.m_Lens.OrthographicSize += (10.5f-10.5f/bigSize)/damping;
            yield return new WaitForFixedUpdate();
        }
    }
}
