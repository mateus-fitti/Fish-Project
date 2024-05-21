using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBehavior : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public float _moveSpeed = 10f;
    public float _friction = 0.1f;
    bool flipped = false;
    Vector2 lookDir = Vector2.right;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude <= 0.5)
        {
            animator.SetTrigger("move");
            rb.AddForce(lookDir * _moveSpeed, ForceMode2D.Impulse);
        } else {
            rb.velocity -= rb.velocity * _friction;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 7)
        {
            lookDir *= -1;
            flipped = !flipped;
            sprite.flipX = flipped;
        }
    }
}
