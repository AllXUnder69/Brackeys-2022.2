using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2[] position = new Vector2[2];
    [SerializeField] float speed = 10;

    int goal = 0;

<<<<<<< Updated upstream:Brackeys 2022.2/Assets/MovingPlatform.cs
    [Space]
    [SerializeField] private float speed;

    [SerializeField] bool hasReachedDestination = false;

    [SerializeField] Vector2 destination;
    Rigidbody2D rb;
    void OnValidate()
=======
    private void Update()
>>>>>>> Stashed changes:Brackeys 2022.2/Assets/_Scripts/MovingPlatform.cs
    {
        if ((Vector2)transform.position == position[goal]) goal = (goal + 1) % position.Length;

        transform.position = Vector2.MoveTowards(transform.position, position[goal], speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position[0], position[position.Length - 1]);
        for (int i = 0; i < position.Length - 1; i++)
        {
<<<<<<< Updated upstream:Brackeys 2022.2/Assets/MovingPlatform.cs
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
=======
            Gizmos.DrawLine(position[i], position[i + 1]);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < position.Length; i++)
        {
            Gizmos.DrawSphere(position[i], 1);
        }
>>>>>>> Stashed changes:Brackeys 2022.2/Assets/_Scripts/MovingPlatform.cs
    }

    [ContextMenu("BringPositionsToLocal")]
    void BringPositionsToLocal()
    {
        for (int i = 0; i < position.Length; i++)
        {
            position[i] += (Vector2)transform.position;
        }
    }
}