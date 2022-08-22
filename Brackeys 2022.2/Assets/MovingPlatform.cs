using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool refresh;

    [SerializeField] private Vector2 initPos;
    [SerializeField] private Vector2 targetPos;
    public Vector2 TargetPos => initPos + targetPos;

    [Space]
    [SerializeField] private float speed;

    [SerializeField] bool hasReachedDestination = false;

    [SerializeField] Vector2 destination;
    Rigidbody2D rb;
    void OnValidate()
    {
        if (refresh)
            refresh = false;

        initPos = transform.position;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        initPos = transform.position;

        destination = initPos;
    }
    void Update()
    {
        hasReachedDestination = CheckIfReached(destination);    

        if (CheckIfReached(destination))
        {
            SwapDestination();
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);   
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(TargetPos, 0.2f);
        Gizmos.DrawWireSphere(initPos, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(initPos, TargetPos);
    }

    void SwapDestination()
    {
        //parvonachalno destination = initPos
        if (destination == initPos)
            destination = TargetPos;
        else if (destination == TargetPos)
            destination = initPos;
    }

    bool CheckIfReached(Vector2 vector)
    {
        return ((Vector2)transform.position - vector).magnitude < 1f;
    }
}