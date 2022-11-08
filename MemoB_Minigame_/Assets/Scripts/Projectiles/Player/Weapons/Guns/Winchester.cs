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
    Animator animator,aniMuzzle;
    LayerMask layerMask;
    protected override void Start()
    {
        base.Start();
        transposer = virtualCamera.GetComponent<CinemachineTransposer>();
        bigPoint = transform.Find("BigPoint").gameObject;
        animator = GetComponent<Animator>();        
        aniMuzzle = transform.Find("AniMuzzle").GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        Aim();
        
        
    }
    protected override void Fire()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Controller.isLife && timer >= interval && Controller.HP - hpCost >= 0)
        {
            timer = 0;
            Controller.hurtByWeapon = true;
            Controller.HP -= hpCost;
            float randomFireAngel;
            randomFireAngel = Random.Range(deflectionAngel, deflectionAngel);
            direction = Quaternion.AngleAxis(randomFireAngel, Vector3.forward) * direction;

            GameObject bullet=ObjectPool.Instance.GetObject(bullet_Prefab);
            bullet.transform.position = muzzle.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<PlayerBullet>().SetBullet(bulletDamage, bulletSpeed, direction);

            // bullet bullet = Instantiate<GameObject>(bullet_Prefab, muzzle.position, Quaternion.identity).GetComponent<bullet>();
            StartCoroutine(PlayFireAni());
            RecoilForce();

        }

    }


    void Aim()
    {
        if (Input.GetMouseButtonDown(1))
        //if (Input.GetKeyDown(KeyCode.J))
        {
            //bigPoint.transform.position = post.transform.position;
            bigPoint.transform.position = new Vector3(direction.x,direction.y,0)*6.5f + Player.transform.position;
            virtualCamera.Follow = bigPoint.transform;
            //virtualCamera.m_Lens.OrthographicSize = 6f/bigSize;
            StartCoroutine(BecomeBigSize(10));
            post.GetComponent<Sight>().sensitivity = 1/bigSize;
            
            Debug.Log("Post");
        }
        if (Input.GetMouseButtonUp(1))
        //if (Input.GetKeyUp(KeyCode.J))
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
            virtualCamera.m_Lens.OrthographicSize += (7f / bigSize-7f) / damping;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ReturnSmallSize(int damping)
    {
        for(int i = 1; i <= damping; i++)
        {
            virtualCamera.m_Lens.OrthographicSize += (7f-7f/bigSize)/damping;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator PlayFireAni()
    {
        aniMuzzle.gameObject.SetActive(true);
        while (aniMuzzle.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.95f)
        {
            yield return new WaitForFixedUpdate();

        }
        aniMuzzle.gameObject.SetActive(false);
        animator.SetTrigger("Fire");

    }
    
}
