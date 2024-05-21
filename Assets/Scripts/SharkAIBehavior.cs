using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkAIBehavior : MonoBehaviour
{
    public float _patrolRange = 100f;
    GameObject player;
    NavMeshAgent agent;
    public LayerMask patrolLayer, playerLayer;
    Vector2 destPoint;
    bool destSet = false;
    Rigidbody2D rb;
    public Animator animator;
    public float _chaseRange = 25f;
    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, _chaseRange, playerLayer);

        if (playerInRange)
            Chase();
        else
            Patrol();

        bool right = true;
        if (transform.rotation.z >= 0.75 || transform.rotation.z < -0.75)
            right = false;

        animator.SetBool("right", right);
        animator.SetTrigger("chasing");
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
        Vector2 lookDir = (Vector2)player.transform.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void Patrol()
    {
        if (!destSet)
            SearchForDest();
        if (destSet)
            agent.SetDestination(destPoint);
        if (Vector2.Distance(transform.position, destPoint) < 10)
            destSet = false;
    }

    void SearchForDest()
    {
        float x = UnityEngine.Random.Range(-_patrolRange, _patrolRange);
        float y = UnityEngine.Random.Range(-_patrolRange, _patrolRange);

        destPoint = new Vector2(transform.position.x + x, transform.position.y + y);

        if (Physics2D.OverlapPoint(destPoint, patrolLayer))
        {
            Vector2 lookDir = destPoint - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            destSet = true;
        }
    }
}
