using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;

    [Space]
    [SerializeField] private UnityEvent E_OnTriggerEnter;
    [SerializeField] private UnityEvent E_OnTriggerExit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            E_OnTriggerEnter?.Invoke();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            E_OnTriggerExit?.Invoke();
    }

    void OnValidate()
    {
        GetComponent<BoxCollider2D>().size = bounds;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}