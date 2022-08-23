using System.Collections;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    [SerializeField] private float distance;
    [SerializeField] private int damage;

    RaycastHit2D hit;
    void Hit()
    {
        hit = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hit)
        {
            StartCoroutine(MakeContact());            
        }
    }

    IEnumerator MakeContact()
    {
        if (_animation)
        {
            _animation.Play();

            yield return new WaitForSeconds(_animation.clip.length);
        }

        Health objectHealth = hit.transform.GetComponent<Health>();

        if (objectHealth != null)
            objectHealth.TakeDamage(damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * distance);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * distance, 1f);
    }
}