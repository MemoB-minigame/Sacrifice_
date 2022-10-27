using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sight : MonoBehaviour
{
    private Transform m_Transform;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;

    public float sensitivity = 1f;

    void Start()
    {
        Cursor.visible = false;
        m_Transform = gameObject.GetComponent<Transform>();

        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        Cursor.visible = false;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Transform.position = mousePositionWorld;

        cinemachineTransposer.m_FollowOffset = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, -10 * 200/sensitivity) / 200*sensitivity;
    }
}