using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//XiaTest
public class PlayerController : MonoBehaviour
{
    //Components
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    //Parameters
    [SerializeField] private float xMoveInputDirection;
    [SerializeField] private float yMoveInputDirection;
    [SerializeField] private float attack;

    //[SerializeField] private float revolverBulletSpeed; ≈≤∂ØµΩŒ‰∆˜¿Ô

    //Move
    [SerializeField] private float speed;
    private int facingDirection = 1;
    private bool isMove = false;

    private bool canSkill = false;

    //HP
    private int hp = 24;
    public bool isLife = true;

    //Prefab
    private GameObject revolverBullet_Prefab;

    //UI
    private TextMeshProUGUI hpNum;

    Vector2 mousePositionWorld;

    public int HP
    {
        get { return hp; }
        set 
        { 
            hp = value;
            hpNum.text = string.Format("{0:D2}", hp);
            if (hp <= 18 && hp > 12)
            {
                Debug.Log("hi");
            }
            else if (hp <= 12 && hp > 6)
            {
                canSkill = false;
            }
            else if (hp <= 6 && hp >= 0)
            {
                canSkill = true;
            }
            else if (hp < 0)
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
        m_Animator = gameObject.GetComponent<Animator>();
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        revolverBullet_Prefab = Resources.Load<GameObject>("Projectiles/Player/RevolverBullet");

        hpNum = GameObject.Find("PlayerInfoCanvas/HP/HPNum").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();

        mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CheckInput()
    {
        xMoveInputDirection = Input.GetAxisRaw("Horizontal");
        yMoveInputDirection = Input.GetAxisRaw("Vertical");

        m_Rigidbody2D.velocity = new Vector2(xMoveInputDirection, yMoveInputDirection).normalized * speed;

        if ((xMoveInputDirection != 0 || yMoveInputDirection != 0) && !isMove) 
        {
            isMove = true;
            if ((facingDirection == -1 && xMoveInputDirection > 0) || (facingDirection == 1 && xMoveInputDirection < 0))
            {
                m_Animator.Play("PlayerRunBack");
            }
            else
            {
                m_Animator.Play("PlayerRun");
            }
        }
        else if (xMoveInputDirection == 0 && yMoveInputDirection == 0 && isMove)
        {
            isMove = false;
            m_Animator.Play("PlayerIdle");
        }
    }

    private void CheckMovementDirection()
    {
        if ((facingDirection == -1 && mousePositionWorld.x - m_Transform.position.x > 0) || (facingDirection == 1 && mousePositionWorld.x - m_Transform.position.x < 0))
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Flip()
    {
        facingDirection *= -1;
        m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
        //m_Transform.localScale = new Vector3(m_Transform.localScale.x * -1, 1, 1);
    }

    private void Dead()
    {
        GameObject.Destroy(gameObject);
    }
}
