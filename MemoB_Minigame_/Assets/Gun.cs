using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    Transform muzzle;
    public GameObject Player;
    public PlayerController Controller;
    public GameObject revolverBullet_Prefab;
    public float revolverBulletSpeed;

    Vector2 direction;
    Vector2 mousePos;

    float flipY,flipX;
    void Start()
    {
        flipX=transform.localScale.x;
        flipY = transform.localScale.y;
        Controller=Player.GetComponent<PlayerController>();
        muzzle=transform.Find("Muzzle");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Fire();
    }
    protected virtual void Shoot()
    {
        mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos-(new Vector2(transform.position.x, transform.position.y))).normalized;
        transform.right = direction;

        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(flipX, flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipX, -flipY, 1);
        }
    }
    protected virtual void Fire()
    {
        if (Input.GetMouseButtonDown(0) && Controller.isLife)
        {
            RevolverBullet revolverBullet = Instantiate<GameObject>(revolverBullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            revolverBullet.SetBullet(revolverBulletSpeed, direction);

            Controller.HP--;
        }
    }
}
