using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("测试")]
    [SerializeField] bool skipDialogue;

    //Components
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    //Parameters
    [SerializeField] private float xMoveInputDirection;
    [SerializeField] private float yMoveInputDirection;
    [SerializeField] private float attack;

    //[SerializeField] private float revolverBulletSpeed; 挪动到武器里

    //Move
    [SerializeField] public float speed;
    private int facingDirection = 1;
    private bool isMove = false;

    private bool canSkill = false;

    //HP
    private int hp = 24;
    public bool isLife = true;

    //Prefab
    private GameObject revolverBullet_Prefab;

    //UI
    private Image hpSlider;
    private Image buff_0;
    private Image buff_1;
    private Image buff_2;
    private SmallHP smallHP;

    private DialogPanelController dialogPanelController;
    private Vector2 mousePositionWorld;

    public int HP
    {
        get { return hp; }
        set 
        { 
            hp = value;
            smallHP.Player_HP = value;  //更新小血条
            //hpNum.text = string.Format("{0:D2}", hp);
            hpSlider.fillAmount = 1 / 24.0f * hp;

            if (hp > 18)
            {
                buff_0.color = Color.white;
            }
            else if (hp <= 18 && hp > 12)
            {
                buff_0.color = Color.yellow;
                buff_1.color = Color.white;
            }
            else if (hp <= 12 && hp > 6)
            {
                canSkill = false;
                buff_1.color = Color.yellow;
                buff_2.color = Color.white;
            }
            else if (hp <= 6 && hp >= 0)
            {
                canSkill = true;
                buff_2.color = Color.yellow;
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

        hpSlider = GameObject.Find("PlayerInfoCanvas/HPText/HPSlider").GetComponent<Image>();
        buff_0 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_0_Image").GetComponent<Image>();
        buff_1 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_1_Image").GetComponent<Image>();
        buff_2 = GameObject.Find("PlayerInfoCanvas/BuffText/Buff_2_Image").GetComponent<Image>();
        smallHP = GameObject.Find("PlayerInfoCanvas/SmallHP").GetComponent<SmallHP>();
        if (GameObject.Find("DialogCanvas/DialogPanel")!=null)
            dialogPanelController = GameObject.Find("DialogCanvas/DialogPanel").GetComponent<DialogPanelController>();
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

        

        if (!dialogPanelController.isSpeaking||skipDialogue)
        {
            m_Rigidbody2D.velocity = new Vector2(xMoveInputDirection, yMoveInputDirection).normalized * speed;

            if ((xMoveInputDirection != 0 || yMoveInputDirection != 0) && !isMove)
            {
                isMove = true;
            }
            else if (xMoveInputDirection == 0 && yMoveInputDirection == 0 && isMove)
            {
                isMove = false;
                m_Animator.Play("PlayerIdle");
            }

            if (isMove)
            {
                if ((facingDirection == -1 && xMoveInputDirection > 0) || (facingDirection == 1 && xMoveInputDirection < 0))
                {
                    m_Animator.Play("PlayerRunBack");
                }
                else
                {
                    m_Animator.Play("PlayerRun");
                }
            }
        }
    }

    private void CheckMovementDirection()
    {
        if (((facingDirection == -1 && mousePositionWorld.x - m_Transform.position.x > 0) || (facingDirection == 1 && mousePositionWorld.x - m_Transform.position.x < 0)) && (!dialogPanelController.isSpeaking||skipDialogue))
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
