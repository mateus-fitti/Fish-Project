using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EggCollision : MonoBehaviour
{
    public Animator animator;
    public LevelController levelController;

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("collect") &&
        animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            levelController.EggCollected();
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            animator.SetTrigger("touch");
            levelController.PlaySound("Collect");

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
