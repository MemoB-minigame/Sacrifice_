using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3AimRayCast : MonoBehaviour
{
    public LineRenderer aimRay;
    public Transform aimRayOrigin;
    public LayerMask layers;
    private Transform player;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        aimRay.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        CastAimRay();
    }

    private void CastAimRay()
    {
        if (isActive)
        {
            aimRay.SetPosition(0, aimRayOrigin.position);
            RaycastHit2D hitInfo = Physics2D.Raycast(aimRayOrigin.position, player.position - aimRayOrigin.position, float.MaxValue, layers);
            if (hitInfo.collider is not null)
            {
                aimRay.SetPosition(1, hitInfo.point);
            }
            else
            {
                aimRay.SetPosition(1, hitInfo.point);
            }
        }
        else
        {
            aimRay.SetPosition(0, Vector3.zero);
            aimRay.SetPosition(1, Vector3.zero);
        }
    }

    public void ActivateAimRay()
    {
        isActive = true;
    }

    public void DeactivateAimRay()
    {
        isActive = false;
    }
}
