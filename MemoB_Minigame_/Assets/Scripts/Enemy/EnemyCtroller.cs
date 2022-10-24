using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtroller : MonoBehaviour
{
    Animator animator;
    new Rigidbody2D rigidbody;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            Debug.Log("Collision Happens");
            rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("Idle");
        }
    }
}
