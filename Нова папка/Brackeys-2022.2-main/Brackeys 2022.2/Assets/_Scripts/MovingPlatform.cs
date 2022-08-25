using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2[] position = new Vector2[2];
    [SerializeField] float speed = 10;

    int goal = 0;

    private void Update()
    {
        if ((Vector2)transform.position == position[goal]) goal = (goal + 1) % position.Length;

        transform.position = Vector2.MoveTowards(transform.position, position[goal], speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position[0], position[position.Length - 1]);
        for (int i = 0; i < position.Length - 1; i++)
        {
            Gizmos.DrawLine(position[i], position[i + 1]);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < position.Length; i++)
        {
            Gizmos.DrawWireSphere(position[i], 1);
        }
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