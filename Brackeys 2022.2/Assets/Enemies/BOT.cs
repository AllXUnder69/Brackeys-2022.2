using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class BOT : MonoBehaviour
{
    //public NavMeshAgent agent;

    [SerializeField] Transform player;

    [SerializeField] Rigidbody2D rigidBoah;

    //[SerializeField] LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    [Header("Patrolling")]
    [SerializeField] Vector2[] patrolPoints;
    [SerializeField] float patrolSpeed;
    bool IsChasing = false;

    /*Vector2 walkPoint;
    bool walkPointSet;
    float walkPointRange;*/

    //Chasing
    [Header("Chasing")]
    [SerializeField] float chaseSpeed;

    //Attacking
    [Header("Attacking")]
    [SerializeField] float attackCooldown = 5;
    bool IsAttacking = false;

    //States
    [SerializeField] float sightRange = 4, attackRange = 2;
    bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        //references
        player = GameObject.FindWithTag("Player").transform;

        if(rigidBoah == null) rigidBoah = GetComponent<Rigidbody2D>();

        //variables
        timeBetweenAttacks = attackCooldown;

        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)player.position, (Vector2)transform.position);
        playerInSightRange = distanceToPlayer <= sightRange;
        playerInAttackRange = distanceToPlayer <= attackRange;

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInSightRange && playerInAttackRange) Attack();
    }

    int goal = 0;
    void Patrol()
    {
        //if last action was chase
        if (IsChasing)
        {
            goal = FindClosestPatrolPoint();
            IsChasing = false;
        }
        else if (Vector2.Distance((Vector2)transform.position, patrolPoints[goal]) <= .1f)
        {
            //change goal to next position
            goal = (goal + 1) % patrolPoints.Length;
        }

        //if last action was attack
        if (IsAttacking)
        {
            timeBetweenAttacks = attackCooldown;
            IsAttacking = false;
        }

        //idle or moove

        //moove
        //Vector2 direction = (patrolPoints[goal] - (Vector2)transform.position).normalized;
        //rigidBoah.AddForce(direction * patrolSpeed * Time.deltaTime, ForceMode2D.Force);

        //to lerp (faster the further away you are from the goal) speed using graph

        transform.position = Vector2.MoveTowards((Vector2)transform.position, patrolPoints[goal], patrolSpeed * Time.deltaTime);
    }

    int FindClosestPatrolPoint()
    {
        float smallestDistance = Vector2.Distance(patrolPoints[0], (Vector2)transform.position);
        int closestPointIndex = 0;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float currentDistance = Vector2.Distance(patrolPoints[i], (Vector2)transform.position);

            if (currentDistance < smallestDistance) 
            {
                smallestDistance = currentDistance;
                closestPointIndex = i;
            }
        }

        return closestPointIndex;
    }

    void Chase()
    {
        IsChasing = true;
        //get angry or something

        //chase player
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)player.position, patrolSpeed * Time.deltaTime);
    }

    float timeBetweenAttacks;
    void Attack()
    {
        IsAttacking = true;
        if(timeBetweenAttacks >= attackCooldown)
        {
            Debug.Log("PEW");
            timeBetweenAttacks = 0;
        }
        else timeBetweenAttacks += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(patrolPoints[0], patrolPoints[patrolPoints.Length - 1]);
        for (int i = 0; i < patrolPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.DrawSphere(patrolPoints[i], 1);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, attackRange);
    }

    [ContextMenu("SetPatrolPointsToBOT")]
    void SetPatrolPointsToBOT()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] += (Vector2)transform.position;
        }
    }
    
    [ContextMenu("ResetPatrolPoints")]
    void ResetPoints()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = (Vector2)transform.position;
        }
    }
}
