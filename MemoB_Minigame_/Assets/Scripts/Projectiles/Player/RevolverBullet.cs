using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBullet : MonoBehaviour
{
    //Components
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;
    private bool isStickBorder = false;
    private bool isAbsorbed = false;
    private int hpRecover=1;

    [SerializeField] private float speed;
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
        if (player_Transform != null)
        {
            if (Vector2.Distance(player_Transform.position, m_Transform.position) < absorbedRadius && isStickBorder && !isAbsorbed)
            {
                isAbsorbed = true;
            }
        }
        

        if (isAbsorbed)
        {
            m_Transform.position = Vector2.MoveTowards(m_Transform.position, player_Transform.position, 0.2f);

            if (m_Transform.position == player_Transform.position)
            {
                Destroy(gameObject);
                player_Transform.gameObject.GetComponent<PlayerController>().HP+=hpRecover;
            }
        }
    }

    public void SetBullet(int n_hpRecover,float speed, Vector2 direction)
    {
        hpRecover = n_hpRecover;
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
