using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;

    [Space]
    [SerializeField] private UnityEvent OnTriggerEnter;
    [SerializeField] private UnityEvent OnTriggerExit;

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnTriggerEnter?.Invoke();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnTriggerExit?.Invoke();
    }

    void OnValidate()
    {
        GetComponent<BoxCollider2D>().size = bounds;
    }
}