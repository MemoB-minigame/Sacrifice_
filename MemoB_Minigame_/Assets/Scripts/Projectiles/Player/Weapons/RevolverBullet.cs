using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBullet : MonoBehaviour
{
    //Components

    private Rigidbody2D rigidbody;
    private bool isStickBorder = false;
    private bool isAbsorbed = false;
    private int hpRecover=1;

    void Awake()
    {    
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*if (player_Transform != null)
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
        }*/
    }

    public void SetBullet(int tmp_hpRecover,float speed, Vector2 direction)
    {
        hpRecover = tmp_hpRecover;
        rigidbody.velocity = direction.normalized * speed ;
        transform.right = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject,0.02f);
    }

    

   
}
