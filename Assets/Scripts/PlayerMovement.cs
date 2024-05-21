using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    public Camera _worldCamera;
    public float _moveSpeed = 5f;
    public float _friction = 0.05f;
    public SpriteRenderer fishSprite;
    public Animator fishAnim;
    Rigidbody2D rb;
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = _worldCamera.ScreenToWorldPoint(Input.mousePosition);
        bool right = true;
        if (transform.rotation.z >= 0.75 || transform.rotation.z < -0.75)
            right = false;

        fishAnim.SetBool("right", right);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if (Input.GetMouseButton(0) && lookDir.magnitude > 1f)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(lookDir.normalized * _moveSpeed, ForceMode2D.Impulse);
        }

        if (rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity * _friction;
        }
    }
}
