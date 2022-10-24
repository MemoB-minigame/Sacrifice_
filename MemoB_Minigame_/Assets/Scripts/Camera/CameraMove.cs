using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject post;
    public GameObject middle;
    CinemachineVirtualCamera virtualCamera;
    CinemachineCameraOffset Moffset;
    Vector3 offset;
    CinemachineTransposer transposer;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        Moffset = GetComponent<CinemachineCameraOffset>();
        transposer=virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        offset = post.transform.position - middle.transform.position;
        transposer.m_FollowOffset = new Vector3(offset.x, offset.y, -10*10)/10;
        
    }
}
