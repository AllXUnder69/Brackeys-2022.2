using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Vector2 idleTarget;
    public Vector2 IdleTarget => (Vector2)Player.position + idleTarget;

    [SerializeField] private float distance;
    [SerializeField] private float waitTime = 2f;

    Vector2 offset;
    //Rigidbody2D rb;
    Transform Player => GameObject.FindWithTag("Player").transform;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r,
            GetComponent<SpriteRenderer>().color.g,
            GetComponent<SpriteRenderer>().color.b,
            255 - ((elapsedTime - Time.time + waitTime) % 255));

        AnimatePosition();

        TeleportControl();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)Player.position + offset, 1);

        Gizmos.color = Color.white;
        Gizmos.DrawLine((Vector2)Player.position, (Vector2)Player.position + offset);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(IdleTarget, 0.2f);
    }

    float elapsedTime = 0f;
    void TeleportControl()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        offset = (cursorPos - (Vector2)transform.position).normalized * distance;

        /*if (Input.GetButtonDown("Fire2"))
            rb.MovePosition((Vector2)transform.position + offset);*/

        if (Input.GetButtonDown("Fire2") && elapsedTime < Time.time)
        {
            Player.position += (Vector3)offset;

            elapsedTime = Time.time + waitTime;
        }
    }
    void AnimatePosition()
    {
        transform.position = Vector3.Lerp(transform.position,
            (Vector3)IdleTarget,
            5 * Time.deltaTime);

        transform.position += Vector3.up * Mathf.Sin(Time.time) * Time.deltaTime;
    }
}