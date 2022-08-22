using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    [SerializeField] private Vector2 bounds;

    void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10f);

        //targetPos.x = Mathf.Clamp(targetPos.x, 0f, bounds.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, 0f, bounds.y);

        if (Application.isPlaying)
            transform.position = Vector3.MoveTowards(transform.position,
                targetPos,
                speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, bounds.x),
            Mathf.Clamp(transform.position.y, 0f, bounds.y), transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(bounds / 2, bounds);
    }
}