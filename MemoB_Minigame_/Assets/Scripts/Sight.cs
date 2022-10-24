using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sight : MonoBehaviour
{
    private Transform m_Transform;
    //private Transform cameraSightOffset_Transform;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        //cameraSightOffset_Transform = GameObject.Find("CameraSightOffset").GetComponent<Transform>();

        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Transform.position = mousePositionWorld;



        //cameraSightOffset_Transform.position = (new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2))/100;

        cinemachineTransposer.m_FollowOffset = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, -10*200)/200;
    }
}
