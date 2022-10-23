using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Components
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;

    //Parameters
    [SerializeField] private float speed;
    [SerializeField] private float xMoveInputDirection;
    [SerializeField] private float yMoveInputDirection;

    [SerializeField] private float revolverBulletSpeed;

    //HP
    private int hp = 12;
    private bool isLife = true;

    //Prefab
    private GameObject revolverBullet_Prefab;

    //UI
    private TextMeshProUGUI hpNum;

    public int HP
    {
        get { return hp; }
        set 
        { 
            hp = value;
            hpNum.text = string.Format("{0:D2}", hp);
            if (hp < 0)
            {
                isLife = false;
                Dead();
            }
        }
    }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        revolverBullet_Prefab = Resources.Load<GameObject>("Projectiles/Player/RevolverBullet");

        hpNum = GameObject.Find("PlayerInfoCanvas/HP/HPNum").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        xMoveInputDirection = Input.GetAxis("Horizontal");
        yMoveInputDirection = Input.GetAxis("Vertical");

        m_Rigidbody2D.velocity = new Vector2(xMoveInputDirection * speed, yMoveInputDirection * speed);

        if (Input.GetMouseButtonDown(0) && isLife)
        {
            RevolverBullet revolverBullet = Instantiate<GameObject>(revolverBullet_Prefab, m_Transform.position, Quaternion.identity).GetComponent<RevolverBullet>();

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            revolverBullet.SetBullet(revolverBulletSpeed, mousePos - m_Transform.position);

            HP--;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
