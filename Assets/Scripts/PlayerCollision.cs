using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    CapsuleCollider2D col;
    Rigidbody2D rb;
    public float _knobackForce = 20f;
    public float _immunityTime = 1f;
    float currentTime = 0f;
    int lifePoints = 3;
    public Sprite[] hearts;
    public Image heartUI;

    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        heartUI.sprite = hearts[3];
    }

    void Update()
    {
        Collider2D tar = Physics2D.OverlapCapsule(transform.position, col.size, CapsuleDirection2D.Horizontal, 0);
        currentTime += Time.deltaTime;

        if (tar.gameObject.layer == 8 && currentTime >= _immunityTime)
        {
            currentTime = 0f;

            Vector2 direction = rb.position - (Vector2)tar.gameObject.transform.position;
            Damage(direction);
        }
    }

    void Damage(Vector2 direction)
    {
        lifePoints--;
        heartUI.sprite = hearts[lifePoints];
        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * _knobackForce, ForceMode2D.Impulse);
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }
}
