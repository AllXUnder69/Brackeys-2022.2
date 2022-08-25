using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    MovingPlatform mp;

    void OnCollisionEnter2D(Collision2D collision)
    {
        mp = collision.transform.GetComponent<MovingPlatform>();

        if (mp != null)
            transform.parent = collision.transform;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (mp != null)
            transform.parent = null;
    }
}