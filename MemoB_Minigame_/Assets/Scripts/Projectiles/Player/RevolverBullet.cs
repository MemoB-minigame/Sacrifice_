using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBullet : MonoBehaviour
{
    //Components
    [SerializeField] private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;

    [SerializeField] private float speed;

    private bool isStickBorder = false;
    private bool isAbsorbed = false;
    [SerializeField] private float absorbedRadius;

    private Transform player_Transform;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        player_Transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector2.Distance(player_Transform.position, m_Transform.position) < absorbedRadius && !isAbsorbed)
        {
            isAbsorbed = true;
        }

        if (isAbsorbed)
        {
            m_Transform.position = Vector2.MoveTowards(m_Transform.position, player_Transform.position, 0.1f);
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
