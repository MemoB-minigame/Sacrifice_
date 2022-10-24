using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform muzzle;
    [SerializeField]GameObject Player;
    PlayerController Controller;
    [SerializeField]GameObject revolverBullet_Prefab;
    [SerializeField]float revolverBulletSpeed;
    [SerializeField] float hardRecoilForce=1;
    [SerializeField] float smoothRecoilForce=1;
    [SerializeField] float jump;
    [SerializeField] float interval=0.384f;
    CinemachineImpulseSource impulse;


    Vector2 direction;
    Vector2 mousePos;
    float timer=0;

    float flipY,flipX;
    void Start()
    {
        flipX=transform.localScale.x;
        flipY = transform.localScale.y;
        Controller=Player.GetComponent<PlayerController>();
        muzzle=transform.Find("Muzzle");
        impulse=GetComponent<CinemachineImpulseSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Fire();
    }
    protected virtual void Shoot()
    {
        timer+=Time.deltaTime;
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
        if (Input.GetMouseButtonDown(0) && Controller.isLife&&timer>=interval)
        {
            timer=0;
            RevolverBullet revolverBullet = Instantiate<GameObject>(revolverBullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<RevolverBullet>();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            revolverBullet.SetBullet(revolverBulletSpeed, direction);

            Controller.HP--;
            impulse.GenerateImpulse();
            StartCoroutine(Recoil(hardRecoilForce,smoothRecoilForce));
        }
    }
    protected virtual void RecoilForce()
    {

    }
    IEnumerator Recoil(float hard,float smooth)
    {
        for(int i=1;i<=3; i++)
        {
            Player.transform.Translate(-direction / 8*hard);
            Player.transform.Translate(Vector3.up/25*jump);
            yield return new WaitForFixedUpdate();
        }
        for (int i = 1; i <= 9; i++)
        {
            Player.transform.Translate(-direction / 24*smooth);
            Player.transform.Translate(Vector3.down / 75 * jump);
            yield return new WaitForFixedUpdate();
        }
    }
}
