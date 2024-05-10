using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{

    public GameObject[] patrolPoints;

    public float speed = 2f;
    public float chaseRange = 3f;

    public enum EnemyState { PATROLLING, CHASING };

    public EnemyState currentState = EnemyState.PATROLLING;

    public GameObject target;
    private GameObject player;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        if(patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned");
        }
        
        target = patrolPoints[currentPatrolPointIndex];

    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        switch(currentState)
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
        }
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    void UpdateState()
    {
        if(isPlayerInChaseRange() && currentState == EnemyState.PATROLLING)
        {
            currentState = EnemyState.CHASING;
        }
        else if(!isPlayerInChaseRange() && currentState == EnemyState.CHASING)
        {
            currentState = EnemyState.PATROLLING;
        }
    }

    bool isPlayerInChaseRange()
    {
        if(player == null)
        {
            Debug.LogError("Player not found");
            return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    void Patrol()
    {
        if(Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = target.transform.position - transform.position;

        direction.Normalize();

        rb.velocity = direction * speed;

        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        if(direction.x < 0)
        {
            sr.flipX = false;
        }
        else if(direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        if(patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach(GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }

}
