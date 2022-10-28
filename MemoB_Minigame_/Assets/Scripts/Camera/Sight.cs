using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sight : MonoBehaviour
{
    private Transform m_Transform;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;
    private float originCameraScale,scaleX,scaleY;
    [SerializeField] Texture2D cursorTexture;
    public float sensitivity = 1f;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(32,32), CursorMode.ForceSoftware);
        //Cursor.visible = false;
        m_Transform = gameObject.GetComponent<Transform>();
        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        originCameraScale=cinemachineVirtualCamera.m_Lens.OrthographicSize;
        scaleX = transform.localScale.x;
        scaleY=transform.localScale.y;
    }

    void Update()
    {
        //Cursor.SetCursor(cursorTexture, new Vector2(512, 512), CursorMode.Auto);
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Transform.position = mousePositionWorld;
        cinemachineTransposer.m_FollowOffset = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, -10 * 200/sensitivity) / 200*sensitivity;
        transform.localScale = new Vector3(scaleX / originCameraScale * cinemachineVirtualCamera.m_Lens.OrthographicSize, scaleY / originCameraScale * cinemachineVirtualCamera.m_Lens.OrthographicSize, 1);
    }
}