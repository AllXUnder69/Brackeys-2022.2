using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] Transform GFX;

    [Space]
    [SerializeField] private KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            transform.position += (Vector3)(offset * ((GFX.rotation.y == 0) ? 1 : -1));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere((Vector2)transform.position + offset, 1);

        Gizmos.color = Color.white;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + offset);
    }
}