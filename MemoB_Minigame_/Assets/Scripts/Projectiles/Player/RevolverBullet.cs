using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBullet : MonoBehaviour
{
    //Components
    
    private Rigidbody2D m_Rigidbody2D;
    private bool isStickBorder = false;
    private bool isAbsorbed = false;
    private float timer;
    [SerializeField] private float absorbedRadius;
    [SerializeField] private float speed;
    [SerializeField] private Transform m_Transform;
    [SerializeField] private float interval=1f;

    private Transform player_Transform;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        player_Transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Vector2.Distance(player_Transform.position, m_Transform.position) < absorbedRadius && !isAbsorbed&&timer>interval)
        {
            isAbsorbed = true;
        }

        if (isAbsorbed)
        {
            m_Transform.position = Vector2.MoveTowards(m_Transform.position, player_Transform.position, 0.1f);
            if (Vector3.Distance(m_Transform.position, player_Transform.position) < 0.2f)
            {
                player_Transform.gameObject.GetComponent<PlayerController>().HP++;
                Destroy(gameObject);
            }
        }
    }

    public void SetBullet(float speed, Vector2 direction)
    {
        m_Rigidbody2D.velocity = direction.normalized * speed ;
        //m_Transform.right = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            isStickBorder = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            isStickBorder = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_Transform.position, absorbedRadius);
    }
}
